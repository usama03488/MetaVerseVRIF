using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Michsky.MUIP;
using YoutubePlayer.Components;
using TMPro;
public class LinkHandler : MonoBehaviour
{
    public GameObject pdfScreen;
    public GameObject youtubeScreen;
    public VideoPlayer videoplayer;
    public ButtonManager VideoPasteButton;
   public GameObject videoObject;
    public TMP_InputField url;
    // Example method to handle the link
    public void HandleLink(string link)
    {
        if (IsPdfLink(link))
        {
            LoadPdf(link);
        }
        else if (IsYouTubeLink(link))
        {
            LoadYouTubeVideo(link);
        }
        else
        {
            Debug.LogError("Unsupported link type. Please provide a valid PDF or YouTube link.");
        }
    }
    public void AddUrl(string link)
    {
        if (IsYouTubeLink(link))
        {
            url.text = link;
        }
      
    }
    // Check if the link is a PDF
    public bool IsPdfLink(string link)
    {
        return link.EndsWith(".pdf", System.StringComparison.OrdinalIgnoreCase);
    }

    // Check if the link is a YouTube link
    public bool IsYouTubeLink(string link)
    {
        return link.Contains("youtube.com/watch") || link.Contains("youtu.be/");
    }

    // Load the PDF on the VR screen
    private void LoadPdf(string link)
    {
        pdfScreen.SetActive(true);
        youtubeScreen.SetActive(false);
        // Replace with your PDF loading implementation
        Debug.Log("Loading PDF: " + link);
        // Add code to load the PDF on the `pdfScreen`.
    }

    public void playVideo()
    {
        videoplayer.Play();
    }
    // Load the YouTube video on the VR screen
    public void LoadYouTubeVideo(string link)
    {
       
        pdfScreen.SetActive(false);
        videoObject.GetComponent<InvidiousVideoPlayer>().ExtractYouTubeVideoId(link);
      
      
        youtubeScreen.SetActive(true);
        videoObject.SetActive(true);
        // Replace with your YouTube loading implementation
        Debug.LogError("Loading YouTube video: " + link);
        // Add code to play the YouTube video on the `youtubeScreen`.
    }
}
