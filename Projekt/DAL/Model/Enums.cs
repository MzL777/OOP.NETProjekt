using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public enum TypeOfEvent
    {
        Goal,
        GoalOwn,
        GoalPenalty,
        RedCard,
        SubstitutionIn,
        SubstitutionOut,
        YellowCard,
        YellowCardSecond
    };

    public enum Position
    {
        Defender,
        Forward,
        Goalie,
        Midfield
    };

    public enum StageName
    {
        Final,
        FirstStage,
        PlayOffForThirdPlace,
        QuarterFinals,
        RoundOf16,
        SemiFinals
    };

    public enum Language
    {
        Hrvatski,
        English
    }
}
