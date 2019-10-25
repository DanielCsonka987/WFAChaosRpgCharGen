using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED SPECIALISTAION LEVEL-COSTS OF A DISCIPLINE
    ///     ->average and benefitial cost, that rises directly proportinally
    ///     ->dscp requirement level riser (some spec level need rises)
    ///     ->attrib requirment value riser (the need of defined attrib requirement rises)
    ///     ->specLevel attributeFeedBack, defines which level, what type of attribute enchant is given
    /// </summary>
    public class CoreOneJPSpecLevel
    {
        public short theSpecLevelMark { get; }
        public short theSpecLevelAverageCost { get; }
        public short theSpecLevelBeneficCost { get; }
        public byte theRequirLevelRisiing { get; }
        public short theRequirAttribRisiing { get; }
        public byte theSpecLevelAttribFeedback { get; }
        /// <summary>
        /// CONSTRUCTOR OF A SPEC LEVEL COST
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="normalCost">normal cost</param>
        /// <param name="beneficCost">benef cost</param>
        /// <param name="levelRise">rising of requDscp level</param>
        /// <param name="attribRise">rising of requirAttrib</param>
        /// <param name="attribFeedbackType">attribFeedback type</param>
        public CoreOneJPSpecLevel(short level, short normalCost, short beneficCost, byte levelRise,
            short attribRise, byte attribFeedbackType)
        {
            theSpecLevelMark = level;
            theSpecLevelAverageCost = normalCost;
            theSpecLevelBeneficCost = beneficCost;

            theRequirLevelRisiing = levelRise;
            theRequirAttribRisiing = attribRise;
            theSpecLevelAttribFeedback = attribFeedbackType;
        }
    }
}
