using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreRepository
{
    /// <summary>
    /// INTERFACE OF JPSCHEMAS AND JP LEVELS
    /// </summary>
    interface ICoreJPLevelsSchemas
    {
        //GET THE BASIC JP FOR A LEVEL
        short findTheNormalLevelAverageJP(byte levelMark); //GETTER OF NORMAL LEVEL BASIC AVERAGE JP
        short findTheNormalLevelBeneficJP(byte levelMark); //GETTER OF NORMAL LEVEL BASIC BENEFICIAL JP
        short findTheSpecLevelAverageJP(byte levelMark);  //GETTER OF SPEC LEVEL BASIC AVERAGE JP
        short findTheSpecLevelBeneficJP(byte levelMark); //GETTER OF SPEC LEVEL BASIC BENEFICIAL JP

        //GET THE EFFECTS OF A LEVEL
        short findTheSchemaRiserForThisNormalLevel(byte levelMark);

        byte findTheRequirementRiserForThisSpecLevel(byte levelMark);
        short findTheAttributeRiserForThisSpec(byte levelMark);

        //CALCULATIING THE PROPER JP TO CORRECT THE BASIC PRICE
        short findSchemaModifForThisJPSchema(short schemaId, short charAttribValue);   //GETTER OF THE PROPER JPSCHEMA MODIFIER
        short findPracticeModifForThisLevel(byte levelMark);    //GETTER OF THE PROPER LEVEL PRACTICE MODIFIRER

        //ATTRUBUTE ENCHANCE MANAGEMENT
        byte findThereAttribFeedbackForThisNormalLevel(byte levelMark);
        byte findThereAttribFeedbackForThisSpecLevel(byte levelMark);
    }
}
