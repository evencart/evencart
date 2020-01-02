using System.Text;

namespace Shippo
{
    public interface IUrlEncoderInfo
    {
        void UrlEncode(StringBuilder sb);
    }
}