using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AkeronStatues
{
    public class CompProperties_HediffEmanator : CompProperties
    {
        public HediffDef hediff;

        public float hediffRadius = 10f;

        public int hediffInterval = 120;

        public CompProperties_HediffEmanator()
        {
            this.compClass = typeof(CompHediffEmanator);
        }
    }
}
