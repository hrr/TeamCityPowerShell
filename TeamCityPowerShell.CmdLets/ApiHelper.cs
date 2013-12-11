﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TeamCityPowerShell.CmdLets
{
    public class ApiHelper
    {
        private static ApiHelper _instance;

        private ApiHelper()
        {
           
        }

        public static ApiHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiHelper();
                }

                return _instance;
            }
        }
        
        public HttpClient HttpClient { get; set; }
        public Uri ApiUri { get; set; }

        public Uri ProjectsUri { get { return new Uri(ApiUri, "httpAuth/app/rest/projects/"); } }
        public Uri BuildTypesUri { get { return new Uri(ApiUri, "httpAuth/app/rest/buildTypes/"); } }
        public Uri ArtifactUri { get { return new Uri(ApiUri, "repository/downloadAll/"); } }
        public string Environment { get; set; }
        public string SelectedProject { get; set; }
        public string SelectedBuildConfiguration { get { return SelectedProject + "_" + Environment; } }
        public string DeployPath { get;set; }

        public Uri GetSaveArtifactUri(string buildConfiguration, string tag)
        {
            return new Uri(ArtifactUri + buildConfiguration + "/" + tag +
                           ".tcbuildtag/.lastPinned");
        }

        public void SaveArtifacts(byte[] artifacts, string buildConfiguration, string tag, string fileName)
        {
            var di = Directory.CreateDirectory(Path.Combine(DeployPath, buildConfiguration, tag));

            File.WriteAllBytes(Path.Combine(di.FullName, fileName), artifacts);
        }
    }
}
