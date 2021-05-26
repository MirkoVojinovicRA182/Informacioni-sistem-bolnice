namespace HospitalInformationSystem.Utility
{
    public class Constants
    {
        public const string STATIC_EQUIPMENT = "Statička";
        public const string DYNAMIC_EQUIPMENT = "Dinamička";
        public const string NEW_ROOM_WINDOW = "NewRoomWindow";
        public const string EDIT_ROOM_WINDOW = "EditRoomWindow";
        public const string ERROR_MESSAGE_BOX_CAPTION = "Greška";
        public const string WARNING_MESSAGE_BOX_CAPTION = "Upozorenje";
        public enum EquipmentSearchParameters
        {
            NAME,
            TYPE,
            MIN_STATE,
            LOCATION
        }
        public const string OPERATION_ROOM = "Operaciona sala";
        public const string OFFICE = "Kancelarija";
        public const string EXAMINATION_ROOM = "Prostorija za preglede";
        public const string REST_ROOM = "Prostorija za odmor";
        public const string HOSPITALIZATION_ROOM = "Sala za hospitalizaciju";
        public const string ROOM_WITH_BEDS = "Soba sa krevetima";
        public const string MAGACINE = "Magacin";
        public const string DILUTION = "Rastvor";
        public const string SYRUP = "Sirup";
        public const string PILL = "Pilula";
        public const string TABLET = "Tableta";
        public const string DATE_TIME_TEMPLATE = "dd.MM.yyyy. HH:mm";
        public const string DATE_TEMPLATE = "dd.MM.yyyy.";
    }
}
