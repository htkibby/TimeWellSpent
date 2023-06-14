const apiUrl = 'https://localhost:7135/api';

export const FetchActivities = async () => {
   const response = await fetch(`${apiUrl}/Activity`);
   const activities = await response.json();
   return activities;
}