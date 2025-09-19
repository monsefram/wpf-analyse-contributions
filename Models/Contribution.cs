using System;
using System.Text;

namespace tpfred.Models
{
    public class Contribution
    {
        private string type;
        private string nom;
        private string prenom;
        private decimal montant;
        private int nbVersements;
        private string municipalite;
        private string codePostal;
        private string parti;
        private string candidat;
        private DateTime? dateEvenement = null;
        private int anneeFinanciere;


        public Contribution(string ligneCsv)
        {
            string[] champs = ligneCsv.Split(";");

            if (champs.Length != 10)
                throw new ArgumentException($"Ligne non valide : {ligneCsv}");

            type = champs[0];

            string[] nomPrenom = champs[1].Split(",");
            nom = nomPrenom[0].Trim();
            prenom = nomPrenom[1].Trim();

            montant = Convert.ToDecimal(champs[2]);
            nbVersements = Convert.ToInt32(champs[3]);

            municipalite = champs[4];
            codePostal = champs[5];

            parti = champs[6];
            candidat = champs[7];

            if (champs[8] != "")
            {
                string[] dateSplit = champs[8].Split("-");
                dateEvenement = new DateTime(
                    Convert.ToInt32(dateSplit[0]),
                    Convert.ToInt32(dateSplit[1]),
                    Convert.ToInt32(dateSplit[2]));
            }

            anneeFinanciere = Convert.ToInt32(champs[9]);
        }


        public string ToCsv()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Type);
            sb.Append(';');

            sb.Append(Nom);
            sb.Append(", ");
            sb.Append(Prenom);
            sb.Append(';');

            sb.Append(Montant);
            sb.Append(';');

            sb.Append(NbVersements);
            sb.Append(';');

            sb.Append(Municipalite);
            sb.Append(';');

            sb.Append(CodePostal);
            sb.Append(';');

            sb.Append(Parti);
            sb.Append(';');

            sb.Append(Candidat);
            sb.Append(';');

            sb.Append(DateEvenement?.ToString("yyyy-MM-dd") ?? "");
            sb.Append(';');

            sb.Append(AnneeFinanciere);

            return sb.ToString();
        }

        public bool EstIllegale
        {
            get
            {
                return Type is "Parti" or "Candidat" or "Député" or "Électeur" && Montant > 200 ||
                        Type == "Campagne" && Montant > 500;
            }
        }

        public string Type => type;

        public string Nom => nom;

        public string Prenom => prenom;

        public decimal Montant => montant;

        public int NbVersements => nbVersements;

        public string Municipalite => municipalite;

        public string CodePostal => codePostal;

        public string Parti => parti;

        public string Candidat => candidat;

        public DateTime? DateEvenement => dateEvenement;

        public int AnneeFinanciere => anneeFinanciere;

        public override string ToString()
        {
            return ToCsv();
        }
    }
}
