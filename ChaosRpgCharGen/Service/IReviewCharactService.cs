using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.GeneralModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.Service
{
    /// <summary>
    /// INTERFACE OF REVIEW SERVICE
    /// </summary>
    interface IReviewCharactService
    {
        DataTable getListOfCharacters();

        List<CoreOneDscpType> collectCoreDscpTypeCollection();
        int addNewCharacterIntoSystem(GeneralOneTrunkEntity newChar, short[] attribValues);

        GeneralOneTrunkEntity findTheExistingCharacter(int charId);
        bool removeExisitingCharacter(int charId);
    }
}
