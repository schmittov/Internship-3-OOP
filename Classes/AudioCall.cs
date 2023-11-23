using Domaci_3.Enums;

namespace Domaci_3.Classes
{
    internal class AudioCall
    {
        public Guid Id { get; }
        public DateTime CallConnectionTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<AudioCallStatus> AudioCallStatuses { get; set; }



        public static void AudioCallStatusChanger(DateTime callConnectionTime, TimeSpan duration)
        {

            if(callConnectionTime+duration>DateTime.Now) { }
            else if (callConnectionTime + duration <= DateTime.Now) { }
        } 
    }
}
