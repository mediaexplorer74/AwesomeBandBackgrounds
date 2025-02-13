﻿namespace BandCentral.Models.Secrets
{
    public static class FlickrConstants
    {
        //filenames
        //public const string BackgroundFavoritesFileName = "BackgroundFavoritesJson.txt";
        public const string FlickrFavoritesFileName = "FavoritesJson.txt";
        public const string FlickrFavoritesBackupFileName = "FavoritesBackupJson.txt";
        public const string ThemeHistoryFileName = "ThemeHistoryJson.txt";

        //flickr
        public const string ApiKey = "";
        public const string WindowsDevFlickrApiKey = "xxxxxxxxx"; // paste your api key here
        public const string WindowsDevFlickrSharedSecret = "xxx"; // paste your secret here
        public const string BaseUrl = "https://api.flickr.com/services/rest/";
        public const string Method = "flickr.photos.getRecent";
        public const string Format = "json";
        public const string Media = "photos";
    }
}