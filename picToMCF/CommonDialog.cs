using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picToMCF
{
    public static class CommonDialog
    {
        /*
         * 上書き確認用ダイアログ
         * @param dataSection データ区分（ファイル｜フォルダ　など）
         * @param dataName データ名
         * 
         */
        public static DialogResult showConfirmMsgOverride(Constants.dataSection dataSection, string dataName)
        {
            return MessageBox.Show("'" + dataName + "'は既に存在します。続行して全て上書きしますか？"
                , dataSection.ToAliasName() + "存在チェック"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Warning
            );

        }
        /*
         * バリデーションエラーメッセージ表示
         * @param dataSection データ区分（ファイル｜フォルダ　など）
         */
        public static void showValidationErrMsg(Constants.dataSection dataSection)
        {
            MessageBox.Show("出力する" + dataSection.ToAliasName() + "名には使用できない文字が入力に含まれています。\n" +
                "以下の文字のみ使用できます。\n" +
                "・半角小文字の英字\n" +
                "・半角数字\n" +
                "・ハイフン\"-\"" +
                "・アンダースコア\"_\""
                , dataSection.ToAliasName() + "名チェック"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Warning
                );
        }
        /*
         * 一般エラーメッセージ表示
         * @param title エラーメッセージタイトル
         * @param msg   エラーメッセージ本文
         */
        public static void showGeneralErrMsg(string title, string msg)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
