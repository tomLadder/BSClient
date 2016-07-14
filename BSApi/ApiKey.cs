using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSApi
{
    public class ApiKey
    {
        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static char[] hexArray = "0123456789abcdef".ToCharArray();

        private static String HashByteArr(byte[] bArr)
        {
            char[] cArr = new char[(bArr.Length * 2)];
            for (int i = 0; i < bArr.Length; i++)
            {
                int i2 = bArr[i] & 255;
                cArr[i * 2] = hexArray[i2 >> 4];
                cArr[(i * 2) + 1] = hexArray[i2 & 15];
            }
            return new String(cArr);
        }

        public static string HashInt(int i)
        {
            int i2;
            int i3 = 0;
            int[] iArr = new int[] { 3790510, 44534180, 97065810, 97731930, 12098130, 20425790, 54763440, 40958180, 82899280, 55954790, 24617580, 6033940, 68901930, 66439560, 95392160, 59670260, 75755530, 2929840, 9829770, 41725310, 23278540, 58381930, 88353420, 68958120, 69080350, 61324790, 71394310, 3848600, 24829090, 5069600, 54510460, 86273980 };
            string[] split = "270.253.215.266.234.265.267.206.273.313.262.272.207.307.226.214.314.211.276.224.254.251.274.275.210.213.216.264.311.317.250.212.246.227.235.256.230.225.260.247.220.315.263.312.223.245.257.306.221.222.231.310.232.252.316.205.255.261.217.271.233.236".Split('.');
            int[] iArr2 = new int[split.Length];
            for (i2 = 0; i2 < split.Length; i2++)
            {
                iArr2[i2] = 255 - Convert.ToInt32(split[i2], 8);
            }
            int[] iArr3 = new int[] { 13758751, 96464, 5630356, 7845695, 5737136, 4105630, 1034022, 3882356, 3894554, 13927633, 737430, 7459069, 7208724, 6072869, 5647788, 2215390, 612700, 12835658, 3618096, 3897092, 5988602, 7849432, 472225, 2894695, 7088807, 121367, 5078804, 4202518, 13071742, 12994983, 1155581, 5741896 };
            string str = "";
            int length = iArr3.Length;
            string str2 = str;
            i2 = 0;
            while (i3 < length)
            {
                str2 = str2 + Char.ToString((char)iArr2[(iArr3[i3] ^ (iArr[(iArr.Length - i2) - 1] / 10)) ^ i]);
                i2++;
                i3++;
            }
            return str2;
        }

        private static String Hash(long l, String str)
        {
            HMACSHA256 secretKeySpec = new HMACSHA256(
                System.Text.Encoding.UTF8.GetBytes(HashInt(5395938)));
            secretKeySpec.Initialize();
            byte[] hash = secretKeySpec.ComputeHash(
                System.Text.Encoding.ASCII.GetBytes(l.ToString() + "/" + str));
            return HashByteArr(hash);
        }

        public static string Generate(String uri)
        {
            long valueOf = ((long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds) / 1000;
            dynamic jSONObject = new JObject();
            jSONObject.public_key = "PgfLa3cGNY5nDN3isibzuGsomSWspjAs";
            jSONObject.timestamp = (int) valueOf;
            jSONObject.hmac = Hash(valueOf, uri);

            return
                Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(Regex.Replace(jSONObject.ToString(), @"\s+", "")));
        }
    }
}
