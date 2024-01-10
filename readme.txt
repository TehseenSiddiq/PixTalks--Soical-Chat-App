## Welcome to the Pixtalks-Group Chat App Repository!

This Unity-based application enables users to engage in group chats with two distinct modes: permanent groups for long-term conversations and secure and intense chat groups that automatically end when all participants leave the room. The app leverages Firebase for database management and utilizes the Photon engine for seamless intense chat experiences.

### Features

1. *Permanent Groups:* Create and join groups for long-term discussions.
2. *Intense Chat Groups:* Set up secure and intense chat rooms that automatically end when all participants leave.
3. *Firebase Integration:* Store and manage group data using Firebase, ensuring seamless synchronization and real-time updates.
4. *Photon Engine:* Enable intense chat functionality through the Photon engine for a smooth and responsive user experience.
5. *User Authentication:* Secure user data with Firebase authentication for a personalized and secure chat environment.

### Scope

Pixtalks target community who are finding a space to talk to their loved ones and friends. Pixtalks also offer a place where more than 10 people can talk to each other, sharing the details for business and everything with any risk of security breach.

### Problem Statement

In a world where communication is increasingly digital, there is a need for a flexible and secure group chat application. Existing solutions often lack the combination of long-term group persistence and intense, secure chat functionality. This app addresses this gap by offering both features in a single, user-friendly package.

### Problem Solution

The GroupChat app solves the problem by allowing users to create and join permanent groups for continuous discussions. Additionally, it provides an intense chat option, ensuring a secure communication environment that automatically ends when all participants leave the room. Leveraging Firebase ensures real-time updates and synchronization of group data, while the Photon engine delivers a smooth and responsive intense chat experience.

### Constraints

- *Platform Compatibility:* The app is limited to platforms supported by Unity, Firebase, and Photon.
- *Internet Connection:* Users need a stable internet connection for real-time communication and data synchronization.

### Functional Requirements

1. *User Authentication:*
   - Users must be able to create accounts and log in securely.
   - Access to specific groups is restricted based on user authentication.

2. *Permanent Groups:*
   - Users can create, join, and leave permanent groups.
   - Group data is stored and synchronized using Firebase.

3. *Intense Chat Groups:*
   - Users can create and join intense chat groups.
   - Chat rooms automatically close when all participants leave.

4. *Firebase Integration:*
   - Group data, including messages and member information, is stored in Firebase.
   - Real-time updates ensure users see the latest information.

5. *Photon Engine Integration:*
   - Photon enables smooth, low-latency communication in intense chat groups.

### Non-functional Requirements

1. *Performance:*
   - The app should provide a responsive and smooth user experience.
   - Intense chat communication should have low latency.

2. *Scalability:*
   - The app should handle a scalable number of users and groups.

3. *Security:*
   - User authentication and data transfer must be secure.
   - Intense chat rooms should be private and secure.

4. *Reliability:*
   - The app should be reliable and available for users whenever they need to communicate.

5. *Usability:*
   - The user interface should be intuitive and user-friendly.
   - Users should be able to navigate easily between permanent and intense chat groups.

### Getting Started

To get started with the GroupChat app, follow these steps:

1. Clone the repository to your local machine:
    bash
    git clone https://github.com/your-username/GroupChatApp.git
    
2. Open the project in Unity.

3. Set up Firebase:
   - Create a Firebase project on the [Firebase Console](https://console.firebase.google.com/).
   - Add your Unity project to the Firebase project.
   - Download and add the Firebase configuration file (google-services.json for Android, GoogleService-Info.plist for iOS) to the project's root folder.

4. Set up Photon:
   - Register for a Photon account and create a new Photon application.
   - Replace the Photon App ID in the project settings with your own.

5. Build and run the app.

### Dependencies

- Unity (version 2022.1.16f)
- Firebase
- Photon (version pun2v)

### Video Link

[Watch the Demo Video](https://youtu.be/x9glrp9o5uk)
