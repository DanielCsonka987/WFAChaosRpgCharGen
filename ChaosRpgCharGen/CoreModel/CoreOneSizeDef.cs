using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED RACE SIZES
    /// </summary>
    public class CoreOneSizeDef
    {
        public byte theSizeId { get; }
        public string theSizeName { get; }
        public byte theBasicSpeedOfSize { get; }
        /// <summary>
        /// CONSTRUCTOR OF A SIZE DEFINITION
        /// </summary>
        /// <param name="sizeId">sizeId</param>
        /// <param name="name">size descr</param>
        /// <param name="quickness">quickness whit that size</param>
        public CoreOneSizeDef(byte sizeId, string name, byte quickness)
        {
            theSizeId = sizeId;
            theSizeName = name;
            theBasicSpeedOfSize = quickness;
        }
    }
}
