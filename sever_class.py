import socket
# Socket is a endpoint that receives data
HEADERSIZE = 10

# Summery:
#   This class sets up a TCP server of the local host
#   If called by main it'll run as normal 
class TcpConnection:
    def __init__(self):
        self.s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.s.bind(('localhost', 1234))
        self.s.listen(5)
        
        self.currentInput = ''

    def GetInput(self):
        return self.currentInput

    def Connect(self):
        # waits for connection from a client
        try:
            self.clientsocket, address = self.s.accept()
            print(f"Connection from {address} has been established")
            msg = "Connection established"
            msg = f'{len(msg):<{HEADERSIZE}}' + msg
            # sends client message on successful connection
            self.clientsocket.send(bytes(msg, "utf-8"))
        except:
            print('Unsuccesful connection from client')

    def Listen(self):
        # after connection waits for message from client socket
        clientmsg = self.clientsocket.recv(16)
        # decodes messag
        clientmsg = clientmsg.decode("utf-8")
        # saves current input
        self.currentInput = clientmsg
        print(f'Message received: {self.GetInput()} and {type(self.GetInput())}')
        # returns message received as a string
        return clientmsg

    def CloseConnection(self):
        if self.s:
            self.s.close()


if __name__ == '__main__':
    connt = TcpConnection()
    connt.Connect()
    while True:
        try:
            connt.Listen()
        except KeyboardInterrupt:
            connt.CloseConnection()
            break
