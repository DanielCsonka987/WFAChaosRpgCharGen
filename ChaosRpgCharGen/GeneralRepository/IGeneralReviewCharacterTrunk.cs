using ChaosRpgCharGen.GeneralModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.GeneralRepository
{
    /// <summary>
    /// INTERFACE OF SYSTEM CHARACTERS
    /// </summary>
    interface IGeneralReviewCharacterTrunk
    {
        List<GeneralOneTrunkEntity> findTheSystemCharacters();

        int addNewCharacterIntoSystem(GeneralOneTrunkEntity newChar, short[] attribValues);
        bool removeCharacterFromSystem(int charId);
    }
}
