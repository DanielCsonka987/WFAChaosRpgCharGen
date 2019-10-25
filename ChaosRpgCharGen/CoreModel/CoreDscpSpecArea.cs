using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF A CORE DEFINED SPECIALISATION AREA OF A DISCIPLINE
    /// </summary>
    public class CoreDscpSpecArea
    {
        public byte theSpecAreaGroup { get; }
        public string theSpecAreaDescr { get; }
        /// <summary>
        /// CONTRUCTOR OF A SPECIALISATION AREA ENTITY
        /// </summary>
        /// <param name="areaGroup">areaFroup</param>
        /// <param name="areaDescr">description</param>
        public CoreDscpSpecArea(byte areaGroup, string areaDescr)
        {
            theSpecAreaGroup = areaGroup;
            theSpecAreaDescr = areaDescr;
        }
    }
}
