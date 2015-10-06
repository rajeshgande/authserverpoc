using System.Collections.Concurrent;
using System.Configuration;

namespace OcsAuthServer.Models
{
    public interface IAudienceRepository
    {
        Audience Get(string audienceId);
        string GetAudienceSecret(string audienceId);

    }

    public class AudienceRepository : IAudienceRepository
    {
        public ConcurrentDictionary<string, Audience> data;

        public AudienceRepository()
        {
            ConcurrentDictionary<string, Audience> data = new ConcurrentDictionary<string, Audience>();
            data.TryAdd(ConfigurationManager.AppSettings["AudienceId"], new Audience()
            {
                AudienceId = ConfigurationManager.AppSettings["AudienceId"],
                AudienceSecret = ConfigurationManager.AppSettings["AudienceSecret"],
            });
        }

        public Audience Get(string audienceId)
        {
            return new Audience()
            {
                AudienceId = ConfigurationManager.AppSettings["AudienceId"],
                AudienceSecret = ConfigurationManager.AppSettings["AudienceSecret"],
            };
        }

        public string GetAudienceSecret(string audienceId)
        {
            if (audienceId == ConfigurationManager.AppSettings["AudienceId"])
            {
                return ConfigurationManager.AppSettings["AudienceSecret"];
            }
         
            throw new System.ArgumentException();
        }
    }
}