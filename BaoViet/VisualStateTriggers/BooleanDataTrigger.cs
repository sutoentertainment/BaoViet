using AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers;
using AdaptiveTriggerLibrary.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.VisualStateTriggers
{
    public class BooleanDataTrigger : AdaptiveTriggerBase<bool, IComparableModifier>,
                                       IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowHeightTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public BooleanDataTrigger()
            : base(new EqualToModifier())
        {
            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool GetCurrentValue()
        {
            return this.Condition;
        }

        #endregion


        ///////////////////////////////////////////////////////////////////
        #region IDynamicTrigger Members

        void IDynamicTrigger.ForceValidation()
        {
            CurrentValue = GetCurrentValue();
        }

        void IDynamicTrigger.SuspendUpdates()
        {
        }

        void IDynamicTrigger.ResumeUpdates()
        {
        }

        #endregion
    }
}
