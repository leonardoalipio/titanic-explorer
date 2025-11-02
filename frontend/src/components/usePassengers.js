import { use, useState } from "react";


const fetchPassengers = fetch("http://127.0.0.1:5012/passengers")
.then((response) => response.json());

const usePassengers = () => {
  
  const passengersResult = use(fetchPassengers);
  const [passengers, setPassengers] = useState(passengersResult)
  
  return {  
    passengers, 
    setPassengers,
  };
}

export default usePassengers;