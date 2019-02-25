using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Enums
    {

        /// <summary>
        /// The operating system that the game is run on.
        /// </summary>
        public enum OperatingSystem
        {
            Windows,
            Mac,
            Linux,
            PS3,
            PS4,
            XBoxOne,
            NintendoSwitch,
            Android,
            IPhone,
            Other
        }

        /// <summary>
        /// The state that says where we are in the game.
        /// </summary>
        public enum GameState
        {
            MainMenu,
            GamePlay,
            Credits
        }

        public enum Direction
        {
            Up,
            Left,
            Down,
            Right
        }

        /// <summary>
        /// Deals with all of the states a timer could be in.
        /// </summary>
        public enum TimerState
        {
            /// <summary>
            /// The timer has been initialized but is not ticking.
            /// </summary>
            Initialized,
            /// <summary>
            /// The timer has started ticking.
            /// </summary>
            Ticking,
            /// <summary>
            /// The timer has finished.
            /// </summary>
            Finished,
            /// <summary>
            /// The timer has stopped.
            /// </summary>
            Stopped,
            /// <summary>
            /// The timer has paused.
            /// </summary>
            Paused
        }

        public enum TimerType
        {
            CountDown,
            CountUp
        }

        public enum Visibility
        {
            Visible,
            Invisible
        }

        public enum ItemType
        {
            Item,
            Pokeball,
            Medicine,
            TMHM,
            Berry,
            KeyItem
        }

        public enum DamageClass
        {
            Physical,
            Special
        }

        public enum Type
        {
            NULL,
            Normal,
            Fighting,
            Flying,
            Poison,
            Ground,
            Rock,
            Bug,
            Ghost,
            Steel,
            Fire,
            Water,
            Grass,
            Electric,
            Psychic,
            Ice,
            Dragon,
            Dark,
            Fairy
        }

        public enum TargetType
        {
            /// <summary>
            /// "One specific move.  How this move is chosen depends upon on the move being used."
            /// </summary>
            [Description("One specific move.  How this move is chosen depends upon on the move being used.")]
            moveSpecific,
            /// <summary>
            /// "One other Pokémon on the field, selected by the trainer.  Stolen moves reuse the same target."
            /// </summary>
            [Description("One other Pokémon on the field, selected by the trainer.  Stolen moves reuse the same target.")]
            meFirst,
            /// <summary>
            /// "The user's ally (if any)."
            /// </summary>
            [Description("The user's ally (if any).")]
            ally,
            /// <summary>
            /// "The user's side of the field.  Affects the user and its ally (if any)."
            /// </summary>
            [Description("The user's side of the field.  Affects the user and its ally (if any).")]
            usersField,
            /// <summary>
            /// "Either the user or its ally, selected by the trainer."
            /// </summary>
            [Description("Either the user or its ally, selected by the trainer.")]
            userOrAlly,
            /// <summary>
            /// The opposing side of the field.  Affects opposing Pokémon."
            /// </summary>
            [Description("The opposing side of the field.  Affects opposing Pokémon.")]
            opponentsField,
            /// <summary>
            /// "The user."
            /// </summary>
            [Description("The user.")]
            user,
            /// <summary>
            /// "One opposing Pokémon, selected at random."
            /// </summary>
            [Description("One opposing Pokémon, selected at random.")]
            randomOpponent,
            /// <summary>
            /// "Every other Pokémon on the field."
            /// </summary>
            [Description("Every other Pokémon on the field.")]
            allOtherPokemon,
            /// <summary>
            /// "One other Pokémon on the field, selected by the trainer."
            /// </summary>
            [Description("One other Pokémon on the field, selected by the trainer.")]
            selectedPokemon,
            /// <summary>
            /// "All opposing Pokémon."
            /// </summary>
            [Description("All opposing Pokémon.")]
            allOpponents,
            /// <summary>
            /// "The entire field.  Affects all Pokémon."
            /// </summary>
            [Description("The entire field.  Affects all Pokémon.")]
            entireField,
            /// <summary>
            /// "The user and its allies."
            /// </summary>
            [Description("The user and its allies.")]
            userAndAllies,
            /// <summary>
            /// "Every Pokémon on the field."
            /// </summary>
            [Description("Every Pokémon on the field.")]
            allPokemon


        }

        public enum ExperienceGrowthRate
        {
            Erratic,
            Fast,
            MediumFast,
            MediumSlow,
            Slow,
            Fluctuating
        }

        /// <summary>
        /// Gets all of the values stored in an enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static Type TypeNameToEnum(string TypeName)
        {
            return (Type)Enum.Parse(typeof(Type), TypeName, true);
        }

        public static TargetType TargetTypeNameToEnum(int TargetID)
        {
            return (TargetType)(TargetID - 1);
        }

        public static T ParseEnum<T>(string EnumValue)
        {
            return (T)Enum.Parse(typeof(T), EnumValue, true);
        }

    }
}
