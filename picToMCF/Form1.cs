using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;

namespace picToMCF {
    public partial class Form1 : Form {//メインウインドウ
        readonly string INIFILE_NAME = "\\picToMCF.ini";
        readonly string PRVFILE_NAME = "\\preview";//プレビュー用画像ファイル名
        readonly string PRFFILE_EXT = ".bmp";//プレビュー用画像ファイル名拡張子
        readonly int MAX_FILENUM = 512;
        string prvPath;
        string appPath;//アプリ用ファイルパス
        string iniFilePath;
        IniFile ini;
        public Form1() {
            InitializeComponent();
            appPath = Application.UserAppDataPath.TrimEnd('\\');
            iniFilePath = appPath + INIFILE_NAME;
            ini = new IniFile(iniFilePath);
        }
        private bool isValidDestFileName(string[] fileNames, string customFileName, bool isCutstomFileName) {
            bool result = true;
            //有効な文字以外を含んでいないかチェックする
            if (isCutstomFileName) {
                result = isValidName(customFileName);
            }
            else {
                foreach(string file in fileNames) {
                    result = isValidName(Path.GetFileNameWithoutExtension(file));
                    if (!result)
                        break;
                }
            }
            return result;
        }
        private bool isValidName(string target)
        {
            return !Regex.IsMatch(target, "[^a-z0-9-_]");
        }
        private void Form1_DragDrop(object sender, DragEventArgs e) {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileNames =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int fileIdx = 1;
            if (fileNames.Length > MAX_FILENUM) {
                CommonDialog.showGeneralErrMsg("入力ファイル数エラー", "(≧Д≦) ＜ " + MAX_FILENUM + "個を超えるファイルの処理は勘弁してください！");
                return;
            }
            if (rbCustomName.Checked && tbDestFileName.Text.Length == 0) {//出力ファイル名の必須チェック
                CommonDialog.showGeneralErrMsg("ファイル名チェック", "出力ファイル名を入力してください。");
                return;
            }
            if (cboxCreateNew.Checked && tbDatapackName.Text.Length == 0)
            {
                //出力フォルダ／データパック名の必須チェック
                string item = rbVer12.Checked ? Constants.dataSection.DIRECTORY.ToAliasName() : Constants.dataSection.DATAPACK.ToAliasName();
                string title = item + "名チェック";
                CommonDialog.showGeneralErrMsg(title, "出力する" + item + "名を入力してください。");
                return;
            }
            // ファイル名チェック
            if (!isValidDestFileName(fileNames, tbDestFileName.Text, rbCustomName.Checked)) {
                // 入力エラーのハイライト
                if (rbCustomName.Checked)
                {
                    tbDestFileName.BackColor = Color.Red;
                    tbDestFileName.ForeColor = Color.White;
                }
                else
                {
                    rbSourceName.BackColor = Color.Red;
                    rbSourceName.ForeColor = Color.White;
                }
                // ダイアログ呼び出し
                CommonDialog.showValidationErrMsg(Constants.dataSection.MCF_FILE);
                return;
            }
            // フォルダ名チェック
            if (!isValidName(tbDatapackName.Text) && cboxCreateNew.Checked)
            {
                // 入力エラーのハイライト
                tbDatapackName.BackColor = Color.Red;
                tbDatapackName.ForeColor = Color.White;
                // ダイアログ呼び出し
                CommonDialog.showValidationErrMsg(Constants.dataSection.DIRECTORY);
                return;
            }
            // チェックOKなのでテキストボックスのハイライトをリセットする
            tbDestFileName.BackColor = Color.White;
            tbDestFileName.ForeColor = Color.Black;
            tbDatapackName.BackColor = Color.White;
            tbDatapackName.ForeColor = Color.Black;
            rbSourceName.BackColor = SystemColors.Control;
            rbSourceName.ForeColor = Color.Black;

            try
            {
                //変換先mcfunctionファイルの出力先ファイルパス
                string destPath = null;
                //処理ステータス
                Constants.procStatus status = Constants.procStatus.DEFAULT;
                //Minecraftバージョン情報
                Constants.mcVersion mcVer;
                //ラジオボタンの値からバージョンを取得
                if (rbVer12.Checked)
                {
                    mcVer = Constants.mcVersion.VER12;
                }
                else if (rbVer13.Checked)
                {
                    mcVer = Constants.mcVersion.VER13;
                }
                else if (rbVer15.Checked)
                {
                    mcVer = Constants.mcVersion.VER15;
                }
                else if (rbVer16.Checked)
                {
                    mcVer = Constants.mcVersion.VER16;
                }
                else
                {
                    mcVer = Constants.mcVersion.VER17;
                }

                // ファイル出力先の指定
                if (rbSourcePath.Checked)
                {
                    destPath = Path.GetDirectoryName(fileNames[0]);
                }
                else
                {
                    destPath = tbOutFolderPath.Text;

                }
                // 「フォルダごと新規作成する」チェックONでフォルダから作成
                if (cboxCreateNew.Checked)
                {
                    string metafilePath = destPath + "\\" + tbDatapackName.Text + "\\pack.mcmeta";
                    destPath += "\\" + tbDatapackName.Text
                                + "\\data\\" + tbDatapackName.Text + "\\functions";
                    DirectoryInfo di = new DirectoryInfo(destPath);
                    
                    if (di.Exists)
                    {
                        //フォルダが存在した場合、上書きするかのダイアログを出す。「はい」なら続行
                        Constants.dataSection dataSection = rbVer12.Checked ? Constants.dataSection.DIRECTORY : Constants.dataSection.DATAPACK;
                        DialogResult result = CommonDialog.showConfirmMsgOverride(dataSection, tbDatapackName.Text);
                        if(result == DialogResult.No)
                        {
                            return;
                        }
                        status = Constants.procStatus.SKIP;
                    }
                    else
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    // pack.mcmetaファイルの入出力
                    var assm = Assembly.GetExecutingAssembly();
                    using (var stream = assm.GetManifestResourceStream("picToMCF.Resources.pack.mcmeta"))
                    {
                        // Streamの内容をすべて読み込んで標準出力に表示する
                        var sr = new StreamReader(stream);
                        System.Text.Encoding enc = new System.Text.UTF8Encoding(false);
                        StreamWriter sw = new System.IO.StreamWriter(metafilePath, false, enc);//次のファイルを生成
                        string buffer;
                        while ((buffer = sr.ReadLine()) != null)
                        {
                            buffer = buffer.Replace("@format",Constants.getPackFormat(mcVer));
                            sw.WriteLine(buffer);
                        }
                        sw.Close();
                    }
                }
                this.AllowDrop = false;
                ConvertFile convert = null;
                // function用ファイル出力処理開始
                foreach (string file in fileNames) {
                    //プレビュー用の画像ファイルの出力先設定
                    prvPath = appPath + PRVFILE_NAME + "_" + String.Format("{0:D3}", fileIdx) + PRFFILE_EXT;
                    //入出力ファイル情報の設定
                    InOutFileInfo inOutDefault = new InOutFileInfo(file, tbDestFileName.Text, prvPath, destPath, fileIdx, fileNames.Length, rbSourceName.Checked);
                    //変換処理の準備
                    convert = new ConvertFile(status, inOutDefault, progressBar, lblProgress);
                    convert.idVer = (int)mcVer;
                    //変換の実行
                    status = convert.convertImg();
                    if (status == Constants.procStatus.ABORT)
                        break;
                    if (cboxPreview.Checked) {
                        FrmPreview view = new FrmPreview(prvPath);
                        view.Show();
                    }
                    fileIdx++;
                }
                this.progressBar.Value = progressBar.Minimum;
                this.lblProgress.Text = "待機中";
            }
            catch (Exception ex) {
                MessageBox.Show("画像のデータ構造が不正です"
                    , "ファイル形式エラー"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning);
                Console.WriteLine(ex.Message);
            }
            finally {
                this.AllowDrop = true;
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.All;
            }
            else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {//画面の初期表示設定
            //バージョンのラジオボタン
            if (ini.GetValue("Radio", "Ver12", "0") == "1") {
                rbVer12.Checked = true;
            }
            if (ini.GetValue("Radio", "Ver13", "0") == "1") {
                rbVer13.Checked = true;
            }
            if (ini.GetValue("Radio", "Ver15", "0") == "1") {
                rbVer15.Checked = true;
            }
            if (ini.GetValue("Radio", "Ver16", "1") == "1") {
                rbVer16.Checked = true;
            }
            if (ini.GetValue("Radio", "Ver17", "1") == "1")
            {
                rbVer17.Checked = true;
            }
            //出力先フォルダの設定
            if (ini.GetValue("Radio", "Default", "1") == "1") {
                rbSourcePath.Checked = true;
                btnBrowse.Enabled = false;
            }
            if (ini.GetValue("Radio", "SelectFolder", "0") == "1") {
                rbSelectFolder.Checked = true;
                btnBrowse.Enabled = true;
            }
            //出力ファイル名の設定
            if (ini.GetValue("Radio", "DefaultName", "1") == "1") {
                rbSourceName.Checked = true;
                tbDestFileName.Enabled = false;
            }
            if (ini.GetValue("Radio", "CustomName", "0") == "1") {
                rbCustomName.Checked = true;
                tbDestFileName.Enabled = true;
            }
            //テキストボックス、チェックボックス設定
            tbOutFolderPath.Text = ini.GetValue("Textbox", "OutFolderPath", System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            tbDestFileName.Text = ini.GetValue("Textbox", "DestFileName", "geoglyph");
            tbDatapackName.Text = ini.GetValue("Textbox", "DatapackName", "geoglyph");
            cboxPreview.Checked = ini.GetValue("Checkbox", "Preview", "1") == "1";
            cboxCreateNew.Checked = ini.GetValue("Checkbox", "CreateNew", "1") == "1";
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //デフォルトは現在入力済みのフォルダパス
            fbd.SelectedPath = tbOutFolderPath.Text;
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK) {
                //選択されたフォルダを表示する
                Console.WriteLine(fbd.SelectedPath);
                tbOutFolderPath.Text = fbd.SelectedPath;
            }
        }

        private void rbDefault_CheckedChanged(object sender, EventArgs e) {
            if (rbSourcePath.Checked == true) {
                btnBrowse.Enabled = false;
            }
            else {
                btnBrowse.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            //終了時に画面上の入力情報をiniファイルに保持する
            ini["Radio", "Ver12"] = rbVer12.Checked ? "1" : "0";
            ini["Radio", "Ver13"] = rbVer13.Checked ? "1" : "0";
            ini["Radio", "Ver15"] = rbVer15.Checked ? "1" : "0";
            ini["Radio", "Ver16"] = rbVer16.Checked ? "1" : "0";
            ini["Radio", "Ver16"] = rbVer17.Checked ? "1" : "0";
            ini["Radio", "Default"] = rbSourcePath.Checked ? "1" : "0";
            ini["Radio", "SelectFolder"] = rbSelectFolder.Checked ? "1" : "0";
            ini["Radio", "DefaultName"] = rbSourceName.Checked ? "1" : "0";
            ini["Radio", "CustomName"] = rbCustomName.Checked ? "1" : "0";
            ini["Textbox", "OutFolderPath"] = tbOutFolderPath.Text;
            ini["Textbox", "DestFileName"] = tbDestFileName.Text;
            ini["Textbox", "DatapackName"] = tbDatapackName.Text;
            ini["Checkbox", "Preview"] = cboxPreview.Checked ? "1" : "0";
            ini["Checkbox", "CreateNew"] = cboxCreateNew.Checked ? "1" : "0";
        }

        private void rbSourceName_CheckedChanged(object sender, EventArgs e) {
            if (rbSourceName.Checked == true) {
                tbDestFileName.Enabled = false;
            }
            else {
                tbDestFileName.Enabled = true;
            }

        }


        private void cboxCreateNew_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxCreateNew.Checked == true)
            {
                tbDatapackName.Enabled = true;
            }
            else
            {
                tbDatapackName.Enabled = false;
            }

        }

        private void tbDestFileName_Leave(object sender, EventArgs e)
        {
            checkTextBoxAndAlert(tbDestFileName, Constants.dataSection.MCF_FILE);
        }

        private void tbDatapackName_Leave(object sender, EventArgs e)
        {
            Constants.dataSection item = rbVer12.Checked ? Constants.dataSection.DIRECTORY : Constants.dataSection.DATAPACK;
            checkTextBoxAndAlert(tbDatapackName, item);
        }
        /*
         * テキストボックスの入力チェック＆警告 
         * @param tb チェックするテキストボックス
         * @param item テキストボックスの項目名
         */
        private void checkTextBoxAndAlert(TextBox tb, Constants.dataSection item)
        {
            if (isValidName(tb.Text))
            {
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
            }
            else
            {
                tb.BackColor = Color.Red;
                tb.ForeColor = Color.White;
                CommonDialog.showValidationErrMsg(item);
            }

        }
    }
}
