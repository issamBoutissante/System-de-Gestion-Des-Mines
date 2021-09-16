namespace Projet_Mines_Official
{
    public class Utilisateur
    {
        public Utilisateur()
        {
            isLogedIn = false;
        }
        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotPass { get; set; }
        public bool isLogedIn { get; set; }
    }
}
