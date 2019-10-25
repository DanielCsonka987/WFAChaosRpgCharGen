using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED JP MODIFIER ATTRIB MIN-MAX
    /// IT DEFINES THE DIFFERENT ATTRIBUTE VALUES AND THE CONNECTED ATTRIB-SCHEMAS
    /// TO DEFINE JP SCHEMA-MODIFIER VALUE (-30%->-15%->0->15%->30%->100%) THAT BECOMES THE ATTRIB-JP-MODIFIER
    ///     via the dscp-attribSchema + levelSchemaAdditioner -> x attribute value in the interval min-max here
    ///     gives the proper modifier (theSchemaModifier)
    /// </summary>
    public class CoreOneJPSchema
    {
        public short theJpSchemaId;     //THE ATTRIB VALUE THAT A DSCP DEFINES/USER CHOOSED
                                        //MODIFIERS UP AND DOWN-THRESHOLDS
        public short theModifIntervalMin;
        public short theModifIntervalMax;
        public short theSchemaModifier;
        /// <summary>
        /// CONSTRUCTOR OF A JPSCHEMA OF JP MODIFIER
        /// </summary>
        /// <param name="jpSchemaId">schemaId</param>
        /// <param name="intervalMin">interval min</param>
        /// <param name="intervalMax">interval max</param>
        /// <param name="modif">modifier value</param>
        public CoreOneJPSchema(short jpSchemaId, short intervalMin, short intervalMax, short modif)
        {
            theJpSchemaId = jpSchemaId;
            theModifIntervalMin = intervalMin;
            theModifIntervalMax = intervalMax;
            theSchemaModifier = modif;
        }
    }
}
