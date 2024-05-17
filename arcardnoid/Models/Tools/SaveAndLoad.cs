using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace arcardnoid.Models.Tools
{
    public class SaveAndLoad<T> where T : class, new()
    {
        #region Private Properties

        private string Filename { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public SaveAndLoad(string filename)
        {
            Filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
        }

        #endregion Public Constructors

        #region Public Methods

        public T Load()
        {
            try
            {
                if (File.Exists(Filename))
                {
                    string content = File.ReadAllText(Filename);
                    return JsonConvert.DeserializeObject<T>(content);
                }
                return new T();
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de lire le fichier {Filename}", ex);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected void Save()
        {
            try
            {
                string content = JsonConvert.SerializeObject(this);
                File.WriteAllText(Filename, content);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de sauvegarder le fichier {Filename}", ex);
            }
        }

        #endregion Protected Methods
    }
}