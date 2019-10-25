using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.GeneralRepository
{
    /// <summary>
    /// DATABASE MANAGING IMPLEMENTATION OF CORE DSCP-BENEFICIAL ADJUST - ORDINARRY AND PROFFESSIONAL DSCP-S
    /// </summary>
    public class GeneralBeneficMediator
    {
        public GeneralBeneficMediator()
        {
        }

        private const string queryToLoadInCoreBeneficConfig =
            "SELECT type_inheritylBeneficial FROM chaos_discipline_type ORDER BY type_id ASC LIMIT 2;";

        public bool[] loadInBeneficConfig()
        {
            try
            {
                bool[] resTemp = new bool[2];

                DataAccess.ConnectToDB();
                List<object[]> result = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInCoreBeneficConfig);
                if (result.Count != 2)
                    throw new GeneralRepositoryException("Nem megfelelő adatelem!");
                resTemp[0] = result[0][0].ToString() == "1" ? true : false;
                resTemp[1] = result[1][0].ToString() == "1" ? true : false;
                return resTemp;
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private const string queryToSaveCoreBenefitConfig =
            "UPDATE chaos_discipline_type SET type_inheritylBeneficial=@config WHERE type_id=@typeId;";

        public void saveBeneficConfig(byte dscpTypeId, bool dscpBeneficState)
        {
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@typeId",dscpTypeId),
                    new KeyValuePair<string, object>("@config",dscpBeneficState?"1":"0")
                };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToSaveCoreBenefitConfig, queryDatas, 2))
                    throw new GeneralRepositoryException("A " + dscpTypeId + ".  képzettségtípus beállítása elmaradt!");
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException("A beállítások mentésénél probléma!\n" + e.Message);
            }
        }
    }
}
