namespace Projet_Mines_Official
{
    public class Borne
    {
        public Borne()
        {
            //Associate Default Values
            this.Borne_X = "";
            this.Borne_Y = "";
        }
        public int BorneId { get; set; }
        public string Borne_X { get; set; }
        public string Borne_Y { get; set; }
    }
}
