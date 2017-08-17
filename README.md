# vuforia-ar-hon-chicago
This application recognizes Chicago's honorary street signs and displays 3D augmented reality content tailored to the sign. The project was built in Unity 3D using the Vuforia platform for image recognition.

![above-sign](https://i.imgur.com/zZw0aSU.png "Object above sign")

## Usage
* Point device camera towards the Chicago honorary street sign
  * For best results, try to position the sign in the very center of the camera
  * The faint arrows
* Keep the device camera stable for 1-2 while Vuforia scans the sign
* When Vuforia has scanned the sign, the user will be able to see a spinning object directly above the sign
  * The object will remain in this position as the user moves around the area
* In order to find out more information about the sign, the user can touch or tap the spinning object
* The user will then be prompted with an alert that contains the name of the sign along with two buttons allowing them to eith er keep examing the sign or to proceed to a webpage that gives a summary of the sign

![alert-menu](https://i.imgur.com/ygaF4pc.png "Alert menu")

## Hierarchy and GameObject Notes
###### Directional Light, Event System
Static, don't change
###### ARCamera
Vuforia Prefab, 
* Camera
Static
###### Directional Light 
###### Directional Light 
