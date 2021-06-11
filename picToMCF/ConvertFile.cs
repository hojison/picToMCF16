using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace picToMCF {
    public class ConvertFile {
        readonly int MAX_CMD_NUM = 65536;//最大コマンド数
        readonly int PIX_SIZ_32 = 4;
        readonly string MCF_EXT = ".mcfunction";
        readonly string[,] WOOLID_TABLE = new string[,] {
            {//羊毛ID名テーブル(ver1.12版)
                 "wool 0",
                 "wool 1",
                 "wool 2",
                 "wool 3",
                 "wool 4",
                 "wool 5",
                 "wool 6",
                 "wool 7",
                 "wool 8",
                 "wool 9",
                 "wool 10",
                 "wool 11",
                 "wool 12",
                 "wool 13",
                 "wool 14",
                 "wool 15"
            },
            {//羊毛ID名テーブル(ver1.13版)
                 "white_wool",
                 "orange_wool",
                 "magenta_wool",
                 "light_blue_wool",
                 "yellow_wool",
                 "lime_wool",
                 "pink_wool",
                 "gray_wool",
                 "light_gray_wool",
                 "cyan_wool",
                 "purple_wool",
                 "blue_wool",
                 "brown_wool",
                 "green_wool",
                 "red_wool",
                 "black_wool" }
        };
        readonly byte[,] WOOLCOLOR_TABLE = new byte[,]{//羊毛ブロックの色テーブル
            { 0xE9, 0xEC, 0xEC},//白色
            { 0xF0, 0x76, 0x13},//オレンジ色
            { 0xBD, 0x44, 0xB3},//赤紫色
            { 0x3A, 0xAF, 0xD9},//空色
            { 0xF8, 0xC6, 0x27},//黄色
            { 0x70, 0xB9, 0x19},//黄緑色
            { 0xED, 0x8D, 0xAC},//桃色
            { 0x3E, 0x44, 0x47},//灰色
            { 0x8E, 0x8E, 0x86},//薄灰色
            { 0x15, 0x89, 0x91},//青緑色
            { 0x79, 0x2A, 0xAC},//紫色
            { 0x35, 0x39, 0x9C},//青色
            { 0x72, 0x47, 0x28},//茶色
            { 0x54, 0x6D, 0x1B},//緑色
            { 0xA1, 0x27, 0x22},//赤色
            { 0x14, 0x15, 0x19},//黒色
        };
        readonly int[] COE = new int[]{ 7, 3, 5, 1 };//拡散誤差係数
        public int idVer = 1;
        InOutFileInfo inOut;
        string destFile;         //出力mcfunctionファイルパス
        string lblmainTail;         //プログレスバー下のラベル文字列後半部
        int destIdx = 1;             //出力ファイルインデックス
        int cmd_cnt = 0;             //コマンド書き込みカウンタ
        ProgressBar pbmain;         //メインウインドウのプログレスバー
        int value0;                 //プログレスバーの初期値
        Label lblmain;              //プログレスバー下のラベル
        Constants.procStatus procStatus = Constants.procStatus.DEFAULT;

        public ConvertFile(Constants.procStatus procStatus, InOutFileInfo inOut, ProgressBar pb, Label lblmain) {
            //ファイル変換設定
            this.inOut = inOut;
            this.pbmain = pb;
            this.lblmain = lblmain;
            this.procStatus = procStatus;
            lblmainTail = "( " + (inOut.fileIdx) + " / " + inOut.srcFileNum +" )";
            value0 = (int)(((double)(inOut.fileIdx-1) / (double)inOut.srcFileNum)*pbmain.Maximum);
        }
        public Constants.procStatus convertImg() {
            Bitmap temp = new Bitmap(inOut.srcFile);
            Bitmap bitmap = null;
            Graphics gr = null;
            try {
                bitmap = new Bitmap(temp.Width, temp.Height); // おんなじサイズのBitmapを作って

                gr = Graphics.FromImage(bitmap);
                gr.DrawImage(temp, 0, 0, bitmap.Width, bitmap.Height); // 絵だけコピー
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            finally {
                gr.Dispose();
                temp.Dispose(); // 後処理
            }
            BitmapData bmpData = null;
            //BOM無しのUTF8でテキストファイルを作成する
            StreamWriter wr = null;
            try {
                PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
                bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, 
                    pixelFormat
                );
                // strideは１次元配列が何個ずつで折り返されているのか　その数
                if (bmpData.Stride < 0) // height * strideで計算するのでマイナスだと困る
                {
                    bitmap.UnlockBits(bmpData);
                    throw new Exception();
                }
                IntPtr ptr = bmpData.Scan0;
                byte[] pixels = new byte[bmpData.Stride * bitmap.Height];
                System.Runtime.InteropServices.Marshal.Copy(ptr, pixels, 0, pixels.Length);
                cmd_cnt = 1;//コマンド書き込み数の初期化
                destIdx = 1;//インデックスの初期化
                System.Text.Encoding enc = new System.Text.UTF8Encoding(false);

                for (int y = 0; y < bmpData.Height; y++) {//ディザリング処理をしながらmcfunctionにコマンド書き込み
                    for (int x = 0; x < bmpData.Width; x++) {
                        if (cmd_cnt == 1) {//新規ファイル作成
                            if (wr != null)//一旦解放して
                                wr.Close();

                            //出力ファイル名を指定し、かつ入力ファイルが複数ある時はファイルインデクスを出力ファイル名に追加する
                            string fileIdxStr = "";
                            if (!inOut.usesSourceName && inOut.srcFileNum > 1)
                            {
                                fileIdxStr = "_" + String.Format("{0:D3}", inOut.fileIdx);
                            }
                            //出力するファイル名の生成
                            destFile = inOut.destFilePathBase + fileIdxStr +"_"+ String.Format("{0:D3}", destIdx) + MCF_EXT;
                            //出力ファイル存在チェック
                            if (System.IO.File.Exists(destFile) && procStatus == Constants.procStatus.DEFAULT)
                            {
                                DialogResult res = CommonDialog.showConfirmMsgOverride(Constants.dataSection.FILE, Path.GetFileName(destFile));
                                if(res == DialogResult.No)
                                {
                                    return Constants.procStatus.ABORT;
                                }
                                procStatus = Constants.procStatus.SKIP;//次回以降はチェックをスキップして上書きするようにする


                            }
                            wr = new System.IO.StreamWriter(destFile, false, enc);//次のファイルを生成
                        }
                        if (x == 0) {
                            //プログレスバーの更新
                            int maxtmp = pbmain.Maximum;
                            pbmain.Maximum = maxtmp + 1;
                            pbmain.Value = 1 + value0 + (int)((double)maxtmp * (y + 1) / bmpData.Height / inOut.srcFileNum);
                            pbmain.Value--;
                            pbmain.Maximum = maxtmp;
                            lblmain.Text = ((int)(100 * pbmain.Value / pbmain.Maximum)).ToString() + "% " + lblmainTail;
                            Application.DoEvents();
                        }
                        //(x,y)のデータ位置
                        int pos = y * bmpData.Stride + x * PIX_SIZ_32;
                        //(x,y)の近傍
                        int[] neighbor = new int[4];
                        neighbor[0] = pos + PIX_SIZ_32;//右
                        neighbor[1] = pos + bmpData.Stride - PIX_SIZ_32;//左下
                        neighbor[2] = pos + bmpData.Stride;//下
                        neighbor[3] = pos + bmpData.Stride + PIX_SIZ_32;//右下
                        // RGB
                        byte b = pixels[pos], g = pixels[pos + 1], r = pixels[pos + 2];
                        int woolIndex;

                        int[] quantError = new int[3];//量子化誤差
                        quantError[0] = b;
                        quantError[1] = g;
                        quantError[2] = r;
                        woolIndex = findClosestWoolColor(ref r,ref g,ref b);//rgbを近似値に変換
                        //mcfunctionファイルへsetblockコマンドを書き込み
                        writeMCF(wr,x,y,bitmap.Width,bitmap.Height, woolIndex);
                        if (cmd_cnt < MAX_CMD_NUM) {
                            cmd_cnt++;
                        }
                        else {
                            cmd_cnt = 1;
                            destIdx++;
                        }
                        quantError[0] -= b;
                        quantError[1] -= g;
                        quantError[2] -= r;
                        for (int q = 0; q < quantError.Length; q++) {//誤差拡散の処理
                            for (int n = 0; n < neighbor.Length; n++) {
                                if (!(x == 0 && n == 1) &&                              //左端のピクセルは左下への拡散をしない
                                    !(x == bmpData.Width - 1 && (n == 0 || n == 3)) &&  //右端のピクセルは右・右下への拡散をしない
                                    neighbor[n] + q < pixels.Length ) {
                                    int newpixel = pixels[neighbor[n] + q] + COE[n] * quantError[q] / 16;
                                    newpixel = newpixel < 0 ? 0 : newpixel;
                                    newpixel = newpixel > 255 ? 255 : newpixel;
                                    pixels[neighbor[n] + q] = (byte)newpixel;
                                }
                            }
                        }

                        pixels[pos] = b; pixels[pos + 1] = g; pixels[pos + 2] = r;
                    }
                }
                // 変更を反映
                System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length);
                //プレビュー画像の保存
                bitmap.Save(inOut.prvFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
                //bitmap.UnlockBits(bmpData);
                return procStatus;

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return Constants.procStatus.ABORT;
            }
            finally {
                //リソースの後始末
                endProcess(wr, bitmap, bmpData);
            }
        }
        /*
         * 終了処理
         * ファイルストリームとビットマップ画像メモリを開放する
         * 
         */
        private void endProcess(StreamWriter wr, Bitmap bitmap, BitmapData bmpData)
        {
            bitmap.UnlockBits(bmpData);
            bitmap.Dispose();
            if (wr != null)
            {
                wr.Close();
            }

        }
        private int findClosestWoolColor(ref byte r, ref byte g, ref byte b) { //最も近い羊毛色に変換
            int closestIdx = 0;
            int closestDist = 3 * 255 * 255 ;//最も近い色との画素値の差
            for (int i = 0; i < WOOLCOLOR_TABLE.GetLength(0); i++) {
                byte tr = WOOLCOLOR_TABLE[i,0], tg = WOOLCOLOR_TABLE[i, 1], tb = WOOLCOLOR_TABLE[i, 2];
                //画素値の差(ユークリッド距離の2乗)
                int dist = (r - tr) * (r - tr) + (g - tg) * (g - tg) + (b - tb) * (b - tb);
                if ( dist < closestDist) {
                    closestDist = dist;
                    closestIdx = i;
                }
            }
            r = WOOLCOLOR_TABLE[closestIdx, 0];
            g = WOOLCOLOR_TABLE[closestIdx, 1];
            b = WOOLCOLOR_TABLE[closestIdx, 2];
            return closestIdx;
        }
        private void writeMCF(StreamWriter wr, int x, int y, int w,int h, int idx) { //x,y: ディザリング後のピクセル座標　idx:羊毛インデックス
            int destX = x - w/2;
            int destZ = y - h/2;
            int section = idVer == (int)Constants.mcVersion.VER12 ? 0 : 1;
            wr.WriteLine("setblock ~" + destX.ToString() + " ~-1 ~" + destZ.ToString() + " " + WOOLID_TABLE[section,idx]);
        }
    }
}
