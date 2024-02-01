using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BattleNETProxy.Models.Responses
{
    public record class Server_token(string Access_token);
    public record class AccessToken(string Access_token);
    public record class Mounts(string Id, string Name);
    public record class MythicRatings(double Rating);


}
