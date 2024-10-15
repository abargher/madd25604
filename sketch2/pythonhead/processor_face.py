import mediapipe as mp
import cv2 as cv
from util import maybeOutput

BaseOptions = mp.tasks.BaseOptions
FaceLandmarker = mp.tasks.vision.FaceLandmarker
FaceLandmarkerOptions = mp.tasks.vision.FaceLandmarkerOptions
VisionRunningMode = mp.tasks.vision.RunningMode
FACE_CONNECTIONS = mp.tasks.vision.FaceLandmarksConnections.FACE_LANDMARKS_CONTOURS

options = FaceLandmarkerOptions(
    base_options=BaseOptions(model_asset_path="./mediapipe_models/face_landmarker.task"),
    running_mode=VisionRunningMode.VIDEO)

landmarker = FaceLandmarker.create_from_options(options)
arguments = {}

frame_idx = 0
def handleFrame(frame):
    global frame_idx
    mp_image = mp.Image(image_format=mp.ImageFormat.SRGB, data=frame)
    face_landmarker_result = landmarker.detect_for_video(mp_image, frame_idx*20)

    vis = frame.copy()

    for face in face_landmarker_result.face_landmarks:
        for landmark in face:
            cv.circle(vis, (int(landmark.x*vis.shape[1]), int(landmark.y*vis.shape[0])), 2, (0, 255, 0), -1)
        for h in FACE_CONNECTIONS:
            cv.line(vis, (int(face[h.start].x*vis.shape[1]), int(face[h.start].y*vis.shape[0])),(int(face[h.end].x*vis.shape[1]), int(face[h.end].y*vis.shape[0])), (0, 255, 0), 1)

    if (not arguments.hide):
         cv.imshow('faces', vis)

    maybeOutput("face", vis, frame_idx)
    frame_idx+=1
    return dict(facepoints=[[(landmark.x, landmark.y, landmark.z) for landmark in face] for face in face_landmarker_result.face_landmarks])

