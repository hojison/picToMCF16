using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace picToMCF {
    public partial class FrmPreview : Form {
        readonly int MINWIDTH = 200;
        readonly int MINHEIGHT = 150;
        Image img;
        public FrmPreview(string file) {
            InitializeComponent();

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            img = Image.FromFile(file);
            Size size = new Size();
            size.Width = (img.Width > MINWIDTH) ? img.Width : MINWIDTH;
            size.Height = (img.Height > MINHEIGHT) ? img.Height : MINHEIGHT;
            this.ClientSize = size;
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pbPreview.Width, pbPreview.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);
            g.DrawImage(img,(pbPreview.Width - img.Width)/2, (pbPreview.Height - img.Height) / 2, img.Width,img.Height);
            pbPreview.Image = canvas;
            img.Dispose();
            g.Dispose();
        }
    }
}
