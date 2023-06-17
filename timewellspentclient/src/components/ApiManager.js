const apiUrl = 'https://localhost:7135/api';

export const FetchActivities = async () => {
   const response = await fetch(`${apiUrl}/Activity`);
   const activities = await response.json();
   return activities;
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