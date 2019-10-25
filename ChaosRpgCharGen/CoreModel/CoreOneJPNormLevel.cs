using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED NORMAL-LEVEL-COSTS OF DISCIPLINES
    ///     ->average and beneficial costs, greater values with level rises
    ///     ->training penalty of each levels
    ///     ->schema addtitiorner, defines the study difficulty of a level (directly proportional)
    ///     and it added to the dscp basic-defined schema value (no matter which attribute that is connected with)
    ///     it affects the JP-cost from attrib connection will be greater with level-rise (attribJPModifier)
    ///     ->attribFeedback -> definses (directly proportionally) what type of attrib-enchant comes from the
    ///     aquired level of dscp
    /// </summary>
    public class CoreOneJPNormLevel
    {
        public byte theLevelMark { get; }
        public short theLevelAverageCost { get; }
        public short theLevelBeneficCost { get; }
        public short thePracticePenalty { get; }        //if dscp studied with practice
        public short theLevelSchemaAdditioner { get; }  //JP Schema modifier at diff. level
        public byte theLevelAttribFeedback { get; }     //0-no feedback 1-only chosen attrib 2-attrib group 3-any
        /// <summary>
        /// CONSTRUCTIOR OF ONE LEVEL COST OF A DISCIPLINE
        /// </summary>
        /// <param name="level">specific level</param>
        /// <param name="normalCost">normal JP</param>
        /// <param name="beneficCost">beneficial jp</param>
        /// <param name="practicePen">penalty if practice</param>
        /// <param name="additioner">additioner at diff level</param>
        /// <param name="feedback">dscp feedback to attrib</param>
        public CoreOneJPNormLevel(byte level,  short additioner, short practicePen, short normalCost, short beneficCost, byte feedback)
        {
            theLevelMark = level;
            theLevelAverageCost = normalCost;
            theLevelBeneficCost = beneficCost;
            thePracticePenalty = practicePen;
            theLevelSchemaAdditioner = additioner;
            theLevelAttribFeedback = feedback;
        }
    }
}
