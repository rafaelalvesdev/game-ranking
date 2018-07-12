using System;
using System.ComponentModel.DataAnnotations;

namespace Game.Ranking.Model
{
    public abstract class ReplicableObject
    {
        [Key]
        public long Key { get; set; }

        public long InsertedTimestamp { get; private set; }
        public long ReplicatedTimestamp { get; private set; }


        public void UpdateInsertedTime()
        {
            InsertedTimestamp = DateTime.Now.Ticks;
        }

        public void UpdateReplicatedTime()
        {
            ReplicatedTimestamp = DateTime.Now.Ticks;
        }
    }
}
