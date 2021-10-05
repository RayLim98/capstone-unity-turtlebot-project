#!/usr/bin/env python
import rospy
import threading
from sever_class import TcpConnection
from ros_controller import Controller

# Summery:
#     This is the main class that:
#         Inits Controller
#         Inits Tcp Sever
# Note: Roscore network must be running in the same local machine this script is runing on 
class MainNode:
  def __init__(self):
    Controller.__init__(self)
    rospy.init_node('talker',anonymous=True)
    rate = rospy.Rate(10)  

    # innit class that handles the controller
    self.controller = Controller()
    # innit class for sever
    self.server = TcpConnection()
    # wait for conenction from client
    self.server.Connect()

    # starts up controller on different thread 
    threading.Thread(target=self.threadServer).start()

    while not rospy.is_shutdown():
        # Control class publishes 
        self.vel_msg = self.controller.run()
        # self.velocity_publisher.publish(self.vel_msg)
        rate.sleep()

  def threadServer(self):
    while not rospy.is_shutdown():
        try:
          # waits and listens for input from the client  
          input = self.server.Listen()
          # after it gets an input - input is fed to the controller to send the command 
          self.controller.get_input(input)
        except KeyboardInterrupt:
          self.server.CloseConnection() 
          break



if __name__ == "__main__":
  try:
    MainNode()
  except rospy.ROSInterrupteException:
      pass

