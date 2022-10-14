using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace AkeronStatues
{
    public class CompHediffEmanator : ThingComp
    {
        protected int ticksUntilNextUpdate = 0;

        public CompProperties_HediffEmanator Props
        {
            get
            {
                return (CompProperties_HediffEmanator)this.props;
            }
        }

        public bool IsActive
        {
            get
            {
                return true;
            }
        }

        protected void ApplyHediff(Pawn pawn)
        {
            Hediff hediff = HediffMaker.MakeHediff(this.Props.hediff, pawn, null);
            hediff.Severity = 1;
            pawn.health.AddHediff(hediff, null, null, null);
        }

        public override void CompTick()
        {
            base.CompTick();
            bool flag = this.Props.hediff != null;
            if (flag)
            {
                this.ticksUntilNextUpdate--;
                bool flag2 = this.ticksUntilNextUpdate < 1;
                if (flag2)
                {
                    bool isActive = this.IsActive;
                    if (isActive)
                    {
                        List<Pawn> freeColonists = this.parent.Map.mapPawns.FreeColonists;
                        IntVec3 position = this.parent.Position;
                        float hediffRadius = this.Props.hediffRadius;
                        for (int i = 0; i < freeColonists.Count; i++)
                        {
                            Pawn pawn = freeColonists[i];
                            bool flag3 = pawn.Position.InHorDistOf(position, hediffRadius); // Checks to see if the pawn is within the radius of this statue's position
                            if (flag3)
                            {
                                this.ApplyHediff(pawn);
                            }
                        }
                    }
                    this.ticksUntilNextUpdate = this.Props.hediffInterval;
                }
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.ticksUntilNextUpdate, "ticksUntilNextUpdate", 0, false);
        }
    }
}
