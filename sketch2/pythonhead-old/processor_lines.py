import sys
import cv2 as cv
from util import maybeOutput
arguments = {}

fld = cv.ximgproc.createFastLineDetector(do_merge=True, distance_threshold=10)

n = 0
def handleFrame(frame):
    global n
    gray = cv.cvtColor(frame, cv.COLOR_BGR2GRAY)
    lines = fld.detect(gray)
    if (lines is not None):
        for i in range(lines.shape[0]):
            pt1 = (int(lines[i][0][0]), int(lines[i][0][1]))
            pt2 = (int(lines[i][0][2]), int(lines[i][0][3]))
            cv.line(gray, pt1, pt2, [255,0,0], 1)

        if (not arguments.hide):
            cv.imshow('lsd_lines', gray)
    
        maybeOutput("lines", gray, n)

        lineso = []
        for i in range(lines.shape[0]):
            ax = lines[i][0][0]/frame.shape[1]
            ay = lines[i][0][1]/frame.shape[0]
            bx = lines[i][0][2]/frame.shape[1]
            by = lines[i][0][3]/frame.shape[0]
            
            lineso += [[[ax,ay],[bx,by]]]

        out = dict(lines=lineso)
        n += 1
        return out
    return dict(lines=[])
