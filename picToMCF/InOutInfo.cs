using System.IO;

namespace picToMCF {
    public class InOutFileInfo {
        //ドラッグ＆ドロップされた入力画像ファイルのパス
        public string srcFile { get; private set; }
        //プレビュー用画像ファイルのパス
        public string prvFilePath { get; private set; }
        //mcfunctionファイルの出力先パス
        public string destFilePathBase { get; private set; }
        //入力画像ファイルのインデックス
        public int fileIdx;
        //入力ファイルの総数
        public int srcFileNum { get; private set; }
        //出力ファイル名に入力ファイル名を使用するか
        public bool usesSourceName { get; private set; }

        /*
         * 初期設定
         * @param srcFile           ドラッグ＆ドロップされた入力画像ファイルのパス
         * @param customFileName    指定した出力ファイル名
         * @param prvFilePath       プレビュー用画像ファイルのパス
         * @param destPath          mcfunctionファイルを出力するフォルダのパス
         * @param fileIdx           入力画像ファイルのインデックス
         * @param srcFileNum        入力ファイルの総数
         * @param usesSourceName    出力ファイル名に入力ファイル名を使用するか
         */
        public InOutFileInfo(string srcFile, string customFileName, string prvFilePath, string destPath, int fileIdx, int srcFileNum, bool usesSourceName) {
            this.srcFile = srcFile;
            this.prvFilePath = prvFilePath;
            this.usesSourceName = usesSourceName;
            this.srcFileNum = srcFileNum;
            this.fileIdx = fileIdx;
            this.usesSourceName = usesSourceName;
            string destFileName;
            if (usesSourceName)
            {
                destFileName = srcFile;
            }
            else
            {
                destFileName = customFileName;
            }
            destFilePathBase = destPath + "\\" + Path.GetFileNameWithoutExtension(destFileName);
        }
        public string getDestFileName() {
            return Path.GetFileNameWithoutExtension(destFilePathBase);
        }
    }
}
