#!/usr/bin/env python
import rospy
from geometry_msgs.msg  import Twist
from datetime import datetime
from math import radians

# Controller class that directly publishes to the Ros Networ
class Controller:
  def __init__(self):
    self.cmd_vel = rospy.Publisher('/cmd_vel', Twist, queue_size=10)
    # self.gameone = True
    self.isRunning = False
    self.input = "0"
    self.fwd_cmd = Twist()
    self.fwd_cmd.linear.x = 0.1

    self.rev_cmd = Twist()
    self.rev_cmd.linear.x = -0.1

    # let's turn at 30 deg/s
    self.left_cmd = Twist()
    self.left_cmd.linear.x = 0
    self.left_cmd.angular.z = radians(35.5)

    self.right_cmd = Twist()
    self.right_cmd.linear.x = 0
    self.right_cmd.angular.z = -radians(35.5)

    self.stop_cmd = Twist()
    self.stop_cmd.linear.x = 0
    self.stop_cmd.linear.y = 0
    self.stop_cmd.angular.z = 0
    
  def get_input(self, input):
    self.input = input

  def call_back(event):
      print("ring")

  def forward(self):
      # set current time
      beginTime = rospy.Time.now()
      # set ros time value for 4 seconds
      duration = rospy.Duration(4)
      # set needed duration
      end = beginTime + duration
      while (rospy.Time.now() < end):
          self.cmd_vel.publish(self.fwd_cmd)
      # reset and publish stop comman
      self.stop()

  def reverse(self):
      # set current time
      beginTime = rospy.Time.now()
      # set ros time value for 4 seconds
      duration = rospy.Duration(4)
      # set needed duration
      end = beginTime + duration
      while (rospy.Time.now() < end):
          self.cmd_vel.publish(self.rev_cmd)
      # reset and publish stop comman
      self.stop()

  def turnright(self):
      # set current time
      beginTime = rospy.Time.now()
      # set ros time value for 4 seconds
      duration = rospy.Duration(4)
      # set needed duration
      end = beginTime + duration
      while (rospy.Time.now() < end):
          self.cmd_vel.publish(self.right_cmd)
      # reset and publish stop comman
      self.stop()

  def turnleft(self):
      # set current time
      beginTime = rospy.Time.now()
      # set ros time value for 4 seconds
      duration = rospy.Duration(4)
      # set needed duration
      end = beginTime + duration
      while (rospy.Time.now() < end):
          self.cmd_vel.publish(self.left_cmd)
      # reset and publish stop comman
      self.stop()

  def stop(self):
      # sends stop command. Resets input so no further command is being sent
      self.cmd_vel.publish(self.stop_cmd)
      self.input = "0"

  # Define inputs for controller
  def run(self): 
    if self.input == "w":
      self.forward() 

    elif self.input == "s":
      self.reverse()

    elif self.input == "d":
      self.turnright()
      
    elif self.input == "a":
      self.turnleft()

    else:
      return self.stop()