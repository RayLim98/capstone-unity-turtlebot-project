# Modified by smartbuilds.io
# Date: 27.09.20
# Desc: This web application serves a motion JPEG stream
# main.py
# import the necessary packages
from flask import Flask, render_template, Response, request, make_response
# from flask_sqlalchemy import SQLAlchemy
from camera import VideoCamera
import time
import threading
import os

pi_camera = VideoCamera(flip=False)  # flip pi camera if upside down.

# App Globals (do not edit)
app = Flask(__name__)


@app.route('/')
def index():
    return render_template('index.html')  # you can customze index.html here


def gen(camera):
    # get camera frame
    while True:
        frame = camera.get_frame()
        yield (b'--frame\r\n'
               b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n\r\n')


@app.route('/video_feed')
def video_feed():
    return Response(gen(pi_camera),
                    mimetype='multipart/x-mixed-replace; boundary=frame')


@app.route('/image.jpg')
def image():
    frame = run(pi_camera)
    response = make_response(frame)
    response.headers.set('Content-Type', 'image/jpeg')
    return response


def run(camera):
    # get camera frame
    # global old_frame
    # old_frame = camera.get_frame()
    # while True:
    frame = camera.get_frame()
    # if len(frame) == 0:
    #     frame = old_frame
    # else:
    #     old_frame = frame
    #time.sleep(0.05)
    return frame


if __name__ == '__main__':
    app.run(host='0.0.0.0', debug=False)
