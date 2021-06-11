using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace picToMCF {
    public class Constants {
        //Minecraftバージョン
        public enum mcVersion {
            VER12,
            VER13,
            VER15,
            VER16,
            VER17
        }
        //処理ステータス
        public enum procStatus
        {
            DEFAULT,
            SKIP,
            ABORT
        }
        //データ区分
        public enum dataSection
        {
            [AliasName("ファイル")]
            FILE,
            [AliasName("mcfunctionファイル")]
            MCF_FILE,
            [AliasName("フォルダ")]
            DIRECTORY,
            [AliasName("データパック")]
            DATAPACK
        }
        public static string getPackFormat(mcVersion ver)
        {
            switch (ver)
            {
                case mcVersion.VER12:
                    return "3";
                case mcVersion.VER13:
                    return "4";
                case mcVersion.VER15:
                    return "5";
                case mcVersion.VER16:
                    return "6";
                case mcVersion.VER17:
                    return "7";
                default:
                    return "7";
            }
        }
    }
}
