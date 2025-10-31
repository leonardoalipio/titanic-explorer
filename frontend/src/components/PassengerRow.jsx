const PassengerRow = ({ passenger }) => {
  return (
    <tr>
        <td className="text-left">{passenger.survived == true ? "Yes" : "No"}</td>
        <td className="text-left">{passenger.pclass}</td>
        <td className="text-left">{passenger.name}</td>
        <td className="text-left">{passenger.sex}</td>
        <td className="text-left">{passenger.age}</td>
        <td className="text-left">{passenger.sibSp}</td>
        <td className="text-left">{passenger.parch}</td>
        <td className="text-left">{passenger.fare}</td>
    </tr>
  );
}

export default PassengerRow;