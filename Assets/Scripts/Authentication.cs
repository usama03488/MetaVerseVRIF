using BNG;
using Paroxe.PdfRenderer;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using YoutubePlayer.Components;

public class Authentication : MonoBehaviour
{
    [HideInInspector]
    public PhotonView view;
    public TMP_Text HeadName;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        GetReference();


        if (view.IsMine)
        {
            string myName = PhotonNetwork.NickName;

            view.RPC(nameof(Sync_RPCName), RpcTarget.AllBuffered, view.ViewID, myName);
            FindObjectOfType<CustomRemotePlayer>().RemotePlayerID = view.ViewID.ToString();

            if (PhotonNetwork.IsMasterClient)
            {
                Destroy(GameObject.Find("Barrier"));

                FindObjectOfType<PDFViewReferenceHolder>().Previous.onClick.AddListener(PDFPreviousBtn);
                FindObjectOfType<PDFViewReferenceHolder>().Next.onClick.AddListener(PDFNextBtn);
                FindObjectOfType<PDFViewReferenceHolder>().LoadDoc.onClick.AddListener(PDFLoadDoc);
                FindObjectOfType<PDFViewReferenceHolder>().LoadButton.onClick.AddListener(PDFLoadDoc);
                FindObjectOfType<PDFViewReferenceHolder>().PasteBtn.onClick.AddListener(PDFPasteBtn);
                FindObjectOfType<PDFViewReferenceHolder>().pasteButton.onClick.AddListener(PDFPasteBtn);

                FindObjectOfType<PDFViewReferenceHolder>().ZoomIn.onClick.AddListener(ZoomIn);
                FindObjectOfType<PDFViewReferenceHolder>().ZoomOut.onClick.AddListener(ZoomOut);
                FindObjectOfType<LinkHandler>().VideoPasteButton.onClick.AddListener(PasteYoutube);




            }
            else
            {
                FindObjectOfType<PDFViewReferenceHolder>().Previous.gameObject.SetActive(false);
                FindObjectOfType<PDFViewReferenceHolder>().Next.gameObject.SetActive(false);
                FindObjectOfType<PDFViewReferenceHolder>().LoadDoc.gameObject.SetActive(false);
                FindObjectOfType<PDFViewReferenceHolder>().PasteBtn.gameObject.SetActive(false);
                FindObjectOfType<PDFViewReferenceHolder>().ZoomIn.gameObject.SetActive(false);
                FindObjectOfType<PDFViewReferenceHolder>().ZoomOut.gameObject.SetActive(false);
            }
        }

    }

    public void ScrollViewSync()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView view = PhotonView.Get(this);
            RectTransform ScollContainerTransform = FindObjectOfType<PDFViewReferenceHolder>().GetComponentInChildren<ScrollRect>().content.GetComponent<RectTransform>();

            view.RPC(nameof(RPC_ScrollView), RpcTarget.All, ScollContainerTransform.anchoredPosition3D, ScollContainerTransform.sizeDelta);
        }
    }
    [PunRPC]
    public void RPC_ScrollView(Vector3 pos, Vector2 size)
    {
      
        if (!PhotonNetwork.IsMasterClient)
        {
            FindObjectOfType<PDFViewReferenceHolder>().GetComponentInChildren<ScrollRect>().content.GetComponent<RectTransform>().anchoredPosition3D = pos;
         //   FindObjectOfType<PDFViewReferenceHolder>().GetComponentInChildren<ScrollRect>().content.GetComponent<RectTransform>().sizeDelta = size;
        }
    }

    public void ZoomIn()
    {
        PhotonView view = PhotonView.Get(this);
        view.RPC(nameof(RPC_ZoomIn), RpcTarget.All);
    }
    [PunRPC]
    public void RPC_ZoomIn()
    {
        FindObjectOfType<PDFViewer>().ZoomIn();
    }
    public void ZoomOut()
    {
        PhotonView view = PhotonView.Get(this);
        view.RPC(nameof(RPC_ZoomOut), RpcTarget.All);
    }
    [PunRPC]
    public void RPC_ZoomOut()
    {
        FindObjectOfType<PDFViewer>().ZoomOut();
    }

    public void PasteYoutube()
    {
        string copiedUrl = GUIUtility.systemCopyBuffer;
        if (FindObjectOfType<LinkHandler>().IsYouTubeLink(copiedUrl) == true)
        {
          
        
            PhotonView view = PhotonView.Get(this);
           FindObjectOfType<LinkHandler>().AddUrl(copiedUrl);
            view.RPC(nameof(Rpc_YoutubePaste), RpcTarget.AllBufferedViaServer, copiedUrl);
         


        }
    }
    public void PDFPasteBtn()
    {
        string copiedUrl = GUIUtility.systemCopyBuffer;
        if (FindObjectOfType<LinkHandler>().IsPdfLink(copiedUrl)==true)
        {
            PhotonView view = PhotonView.Get(this);
            FindObjectOfType<PDFViewReferenceHolder>().Input.text = copiedUrl;
            view.RPC(nameof(RPC_PDFPasteBtn), RpcTarget.AllBufferedViaServer, copiedUrl);
        }
     
    }

    [PunRPC]
    public void RPC_PDFPasteBtn(string Link)
    {
      
        if (!string.IsNullOrEmpty(Link))
        {

            FindObjectOfType<PDFViewer>().FileURL = Link;
        }

    }
    [PunRPC]
    public void Rpc_YoutubePaste(string Link)
    {
       FindObjectOfType<LinkHandler>().LoadYouTubeVideo(Link);
        
    }
    [PunRPC]
    public void YouTubeLoad()
    {

    }

    public void PDFLoadDoc()
    {
        PhotonView view = PhotonView.Get(this);
        view.RPC(nameof(RPC_PDFLoadDoc), RpcTarget.AllBufferedViaServer);
    }
    [PunRPC]
    public void RPC_PDFLoadDoc()
    {
        FindObjectOfType<PDFViewer>().LoadDocument();
    }


    public void PDFPreviousBtn()
    {
        PhotonView view = PhotonView.Get(this);
        int page =  FindObjectOfType<PDFViewer>().GetMostVisiblePageIndex();
      view.RPC(nameof(RPC_PDFPreviousBtn), RpcTarget.AllBufferedViaServer, page);
    }
    [PunRPC]
    public void RPC_PDFPreviousBtn(int page)
    {
        FindObjectOfType<PDFViewer>().GoToPreviousPage(page);
    }
    public void PDFNextBtn()
    {
        PhotonView view = PhotonView.Get(this);
        int page = FindObjectOfType<PDFViewer>().GetMostVisiblePageIndex();
        view.RPC(nameof(RPC_PDFNextBtn), RpcTarget.AllBufferedViaServer,page);
    }
    [PunRPC]
    public void RPC_PDFNextBtn(int page)
    {
        FindObjectOfType<PDFViewer>().GoToNextPage(page);
    }

    void GetReference()
    {
        if(view.IsMine)
        {
            foreach (var _Gref in FindObjectOfType<ReferenceHolder>().TeleportToRoom)
            {
                _Gref.SetActive(true);
            }
        }
       
        
    }

    [PunRPC]
    public void Sync_RPCName(int ClientID,string name,PhotonMessageInfo info)
    {
       int _ClientID = PhotonView.Find(ClientID).ViewID;
        Debug.Log(PhotonView.Find(ClientID).name + " " + name);
        /*if (GetComponent<PhotonView>().ViewID == _ClientID)
        {*/
            PhotonView.Find(ClientID).GetComponent<Authentication>().HeadName.text = name;
     /*   }*/
    }


}
