using System;

namespace XamarinSeed.Model.Entity
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
    }
}
