using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Global Variable for IP
    public static string IP;
    // if nothing is inputed
    public static string DefaultIP;
    //Tcp
	public static TcpClient socketConnection; 	
    //
	public static Thread clientReceiveThread; 	
	public static NetworkStream stream; 	

    void Start()
    {
        // For input field is left unchecked or empty
        // if you're not bothered type in the input alter this instead
        DefaultIP = "http://192.168.1.35:5000/image.jpg";
    }
    public void StartGame() {
		// On start game connect to server
        ConnectToTcpServer();
        SceneManager.LoadScene(1);
    }

    // For input field Onchange
    public void SaveIpInput(string inputIp) {
       IP = "http://" + inputIp + "/image.jpg";
    }
	public void ConnectToTcpServer () {
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e);
		} 	
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			// Ip address the TCP server is running on
			socketConnection = new TcpClient("127.0.0.1", 1234);  			
			Byte[] bytes = new Byte[16];             
			while (true) { 				
				// Get a stream object for reading 				
					int length; 					
					stream = socketConnection.GetStream();
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData); 						
						Debug.Log("server message received as: " + serverMessage); 					
					} 				
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
}
