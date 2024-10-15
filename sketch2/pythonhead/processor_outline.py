import mediapipe as mp
import cv2 as cv
from util import maybeOutput
from ultralytics import YOLO, SAM

model = YOLO('yolov8n-seg.pt')
# model = SAM('mobile_sam.pt')

# Display model information (optional)
model.info()

arguments = {}

frame_idx = 0
def handleFrame(frame):
    global frame_idx

    vis = frame.copy()
    results = model(frame, retina_masks=True, classes=[0,15])  # list of Results objects

    if (not arguments.hide):
        if (len(results)>0):
            for r in results:
                # print("R = %s" % r.masks)
                vis[:] = vis[:]*0.0
                vis = r.plot(img=vis, masks=True, boxes=False)  # plot a BGR numpy array of predictions
        cv.imshow('outline', vis)

    maybeOutput("outline", vis, frame_idx)
    frame_idx += 1

    # for n in results[0].masks[0].xyn[0]:
    #      print(n)

    if (results and len(results) and results[0].masks):
        return dict(outline=[[[float(x),float(y)] for (x,y) in m.xy[0]] for m in results[0].masks])
    return dict(outline=[])

