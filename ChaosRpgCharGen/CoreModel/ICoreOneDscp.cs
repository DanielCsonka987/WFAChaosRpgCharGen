using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// INTERFACE OF MODEL-REPO CORE DSCP-S
    /// </summary>
    interface ICoreOneDscp
    {
        

        bool isTheRequriementSevere();
        bool mentorIsNeededToStudy();

        List<CoreDscpAttribRequir> collectAllAttribs();

        bool isThereANormRequirement();
        CoreDscpNormalRequir findSeekedDscpRequir(byte groupId);
        List<CoreDscpNormalRequir> collectAllRequir();

        bool isThereSpecialisation();
        CoreDscpSpec findSeekedSpecRequirement_Entity(byte groupId);
        byte findSeekedSpecRequirement_GroupId(short[] elementId, byte[] types);
        List<CoreDscpSpecArea> collectAllSpecialisationArea();
        string findSeekedSpecAreaText(byte areaId);

        short findTheSeekedAttribSchemaValue(byte attribId);
    }
}
