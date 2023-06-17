const apiUrl = 'https://localhost:7135/api';

export const FetchActivities = async () => {
   const response = await fetch(`${apiUrl}/Activity`);
   const activities = await response.json();
   return activities;
}

export const FetchActivityById = async (id) => {
   const response = await fetch(`${apiUrl}/Activity/${id}`);
   const activity = await response.json();
   return activity;
}

export const FetchActivitiesByUserEmail = async (email) => {
   const response = await fetch(`${apiUrl}/Activity/user/${email}`);
   const activities = response.json();
   return activities
}

export const FetchUserToActivities = async () => {
   const response = await fetch(`${apiUrl}/UserToActivity`);
   const uats = await response.json();
   return uats;
}

export const FetchMoods = async () => {
   const response = await fetch(`${apiUrl}/Mood`);
   const mood = await response.json();
   return mood;
}

export const FetchUsers = async () => {
   const response = await fetch(`${apiUrl}/User`);
   const users = await response.json();
   return users;
}

export const FetchUserById = async (id) => {
   const response = await fetch(`${apiUrl}/User/${id}`);
   const user = await response.json();
   return user;
}

export const FetchWeeks = async () => {
   const response = await fetch(`${apiUrl}/Week`);
   const weeks = await response.json();
   return weeks;
}

export const FetchCategories = async () => {
   const response = await fetch(`${apiUrl}/Category`);
   const categories = await response.json();
   return categories;
}

export const AddUta = async (utaObject) => {
   const options = {
      method: "POST",
      headers: {
         "Content-Type": "application/json"
      },
      body: JSON.stringify(utaObject)
   };
   await fetch (`${apiUrl}/UserToActivity`, options)
};

export const AddActivity = async (activityObject) => {
   const options = {
      method: "POST",
      headers: {
         "Content-Type": "application/json"
      },
      body: JSON.stringify(activityObject)
   };
   await fetch (`${apiUrl}/Activity`, options)
};

export const PutUta = async (utaObject) => {
   await fetch (`${apiUrl}/${utaObject.id}`, {
      method : "PUT",
      headers : {
         "Content=Type" : "application/json"
      },
      body : JSON.stringify(utaObject)
   })
}