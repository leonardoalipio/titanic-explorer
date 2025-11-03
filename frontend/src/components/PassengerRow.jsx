const PassengerRow = ({ passenger }) => {
  return (
    <tr>
        <td className="text-left">{passenger.survived == 1 ? "Yes" : "No"}</td>
        <td className="text-left">{passenger.pclass}</td>
        <td className="text-left">{passenger.name}</td>
        <td className="text-left">{passenger.sex == 0 ? "Male" : "Female"}</td>
        <td className="text-left">{passenger.age}</td>
        <td className="text-left">{passenger.sibSp}</td>
        <td className="text-left">{passenger.parch}</td>
        <td className="text-left">{passenger.fare}</td>
    </tr>
  );
}

export default PassengerRow;