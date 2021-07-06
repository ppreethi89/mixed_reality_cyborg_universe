using System.Collections.Generic;
using UnityEngine;


public static class ArtistInfo
{
    public static int maxPage;
    private static string appselected;
    private static string themeselected;
    private static string galleryselected = "Style";
    private static bool photomosaicselected = false;
    private static Texture cropimageselected = null;
    private static Texture compositionimageselected = null;
    private static Texture styleimageselected = null;
    private static Texture colorimageselected = null;
    private static List<string> compositionimagesnames = new List<string>();
    private static string url;
    private static List<Texture> compositionimagestextures = new List<Texture>();
    private static string azurestorageaccount = "cyborguniverse";
    private static string azureaccesskey = "Bsu0QC1pGIdHIG6OJ9W4vHz3oN68M9L8QI4crjGuz6sBOJZ5KoAp8UEE/XM86Ot7T62e3vSJCGGmHxzFFxTlpQ==";
    private static Texture2D toenailArtImage;
    private static Texture2D thumbnailArtImage;

    public static Texture2D ToenailArtImage {

        get
        {
            return toenailArtImage;
        }

        set
        {
            toenailArtImage = value;
        }
    }

    public static Texture2D ThumbnailArtImage
    {

        get
        {
            return thumbnailArtImage;
        }

        set
        {
            thumbnailArtImage = value;
        }
    }


    public static Texture StyleImageSelected {

        get
        {
            return styleimageselected;
        }

        set
        {
            styleimageselected = value;
        }
    }
    
    public static Texture ColorImageSelected {

        get
        {
            return colorimageselected;
        }

        set
        {
            colorimageselected = value;
        }

    } 
    
    public static string AzureStorageAccount {

        get 
        {
            return azurestorageaccount;
        }
    
    }

    public static string AzureAccessKey
    {

        get
        {
            return azureaccesskey;
        }

    }


    public static List<Texture> CompositionImagesTextures {

        get
        {
            return compositionimagestextures;

        }

        set
        {
            compositionimagestextures = value;

        }

    }


    public static string URL {

        get
        {
            return url;

        }
        set
        {
            url = value;

        }

    }

    public static List<string> CompositionImagesNames
    {
        get
        {
            return compositionimagesnames;

        }
        set
        {
            compositionimagesnames = value;

        }
    }

    public static Texture CompositionImageSelected {

        get {
            return compositionimageselected;

        }

        set {

            compositionimageselected = value;        
        }

    }



    public static bool PhotomosaicSelected
    {
        get
        {
            return photomosaicselected;
        }
        set
        {
            photomosaicselected = value;
        }
    }

    public static Texture CropImageSelected
    {
        get
        {
            return cropimageselected;
        }
        set
        {
           cropimageselected = value;
        }
    }


    public static string AppSelected
    {
        get
        {
            return appselected;
        }
        set
        {
            appselected = value;
        }
    }

    public static string ThemeSelected
    {
        get
        {
            return themeselected;
        }
        set
        {
           themeselected = value;
        }
    }

    public static string GallerySelected
    {

        get
        {
            return galleryselected;
        }

        set
        {
            galleryselected = value;
        }

    }

}
