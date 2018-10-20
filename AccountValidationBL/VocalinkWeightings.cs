using AccountValidationBL.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AccountValidationBL
{
    // Note - this would be loaded once, cached / singleton.
    internal class VocalinkModulusWeightingData
    {
        const string VOCALINK_DATA_URL = "https://www.vocalink.com/media/3034/valacdos.txt";
        private bool WeightingsLoaded { get; set; }
        private List<ModulusWeighting> ModulusWeightings { get; set; }

        internal void DownloadVocalinkData()
        {
            string data = new WebClient().DownloadString(VOCALINK_DATA_URL);
            string[] rows = data.Split('\r');
            var modulusWeightings = new List<ModulusWeighting>();

            foreach (var r in rows)
            {
                string[] rowData = r.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (1 == rowData.Length) { break; }
                var modulusWeighting = new ModulusWeighting();

                modulusWeighting.SortCodeStart = double.Parse(rowData[0]);
                modulusWeighting.SortCodeEnd = double.Parse(rowData[1]);
                modulusWeighting.ModulusAlgorithmEnum = (ModulusAlgorithmEnum)Enum.Parse(typeof(ModulusAlgorithmEnum), rowData[2], true);

                var weights = new int[14];
                for (var i = 3; i < 17; i++)
                {
                    weights[i - 3] = int.Parse(rowData[i]);
                }
                modulusWeighting.Weights = weights;

                if (rowData.Length == 18)
                {
                    modulusWeighting.ExceptionNumber = int.Parse(rowData[17]);
                }
                modulusWeightings.Add(modulusWeighting);
            }
            this.ModulusWeightings = modulusWeightings;
            this.WeightingsLoaded = true;
        }

        internal List<ModulusWeighting> GetModulusWeightings()
        {
            if (!this.WeightingsLoaded)
            {
                this.DownloadVocalinkData();
            }
            return this.ModulusWeightings;
        }

    }
}
