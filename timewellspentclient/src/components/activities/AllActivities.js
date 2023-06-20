import { useEffect, useState } from "react"
import { FetchActivities } from "../ApiManager"
import { Button, Card, CardActions, CardContent, CardMedia, Grid, Typography } from "@mui/material"

export const AllActivities = () => {
   const [activities, setActivities] = useState([])

   const fetchActivites = async () => {
      const activitiesFromApi = await FetchActivities()
      setActivities(activitiesFromApi)
   }

   useEffect(() => {
      fetchActivites()
   }, [])

   return (
      <>
      <Grid container spacing={2}>
         {activities.map(
            (activity) => {
               return (
                  <Grid item xs={3}>
                     <Card sx={{ maxWidth: 275 }} key={`activity--${activity.id}`} variant="outlined">
                        <CardContent>
                           <Typography sx={{ fontSize: 28 }} color="text.primary" gutterBottom>
                              {activity.name}
                           </Typography>
                           <CardMedia
                              component="img"
                              image={activity.image}
                              height="200" 
                              />
                        </CardContent>
                        <CardActions>
                           <Button size="small" href={`/activitydeatils/${activity.id}`} >Learn More</Button>
                        </CardActions>
                     </Card>
                  </Grid>
               )
            }
         )}
      </Grid>
      </>
   )
}