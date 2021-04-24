pragma solidity ^0.6.2;

import "../libs/SafeMath.sol";
import "../interfaces/ERC20Interface.sol";

/// @dev Privreda Token
contract PrivredaToken is ERC20Interface {
    using SafeMath for uint256;

    // ERC20 compatible total supply
    uint256 public override totalSupply = 0;

    // Name of the token contract
    string public override constant name = "Hack 4 Privreda Token";

    // Symbol of the token contract
    string public override constant symbol = "H4P";

    // Number of token's decimals
    uint8 public override constant decimals = 18;
    
    // Flag indicating if transfers are enabled
    bool public transfersEnabled = true;

    address _owner;

    // mapping for balances of addresses
    mapping(address => uint256) balances;

    // allowance mapping
    mapping (address => mapping (address => uint256)) internal allowed;

    /// @notice Owner only modifier
    modifier _ownerOnly() { 
        require(msg.sender == _owner);
        _;
    }

    constructor () public {
        _owner = msg.sender;
    }
    
    function allowance(address owner, address spender) public view override returns (uint256){
        return 1;
    }

    /// @notice Enables token holders to transfer their tokens freely if true
    /// @param _transfersEnabled True if transfers are allowed in the clone
    function enableTransfers(bool _transfersEnabled) _ownerOnly public {
        transfersEnabled = _transfersEnabled;
    }
     
    /// @notice Transfer token for a specified address
    /// @param _to The address to transfer to.
    /// @param _value The amount to be transferred.
    function transfer(address _to, uint256 _value) public override _ownerOnly returns (bool) {
        require(transfersEnabled);
        _transfer(msg.sender, _to, _value);
        return true;
    }

    /// @notice Gets the balance of the specified address.
    /// @param wallet The address to query the the balance of.
    /// @return balance An uint256 representing the amount owned by the passed address.
    function balanceOf(address wallet) public override view returns (uint256 balance) {
        return balances[wallet];
    }
  
    /// @dev Internal function to transfer tokens from one address to another
    /// @param _from address The address which you want to send tokens from
    /// @param _to address The address which you want to transfer to
    /// @param _value uint256 the amount of tokens to be transferred
    function _transfer(address _from, address _to, uint256 _value) internal {
        require(_to != address(0));
        require(_value <= balances[_from]);
        require(balances[_to] + _value > balances[_to]); // Overflow check

        balances[_from] = balances[_from].sub(_value);
        balances[_to] = balances[_to].add(_value);
    }

    /// @notice Transfer tokens from one address to another
    /// @param _from address The address which you want to send tokens from
    /// @param _to address The address which you want to transfer to
    /// @param _value uint256 the amount of tokens to be transferred
    /// @return True if transfer successful
    function transferFrom(address _from, address _to, uint256 _value) public override _ownerOnly returns (bool) {
        _transfer(_from, _to, _value);
        return true;
    }

    /// @notice Approve the passed address to spend the specified amount of tokens on behalf of msg.sender.
    /// Beware that changing an allowance with this method brings the risk that someone may use both the old
    /// and the new allowance by unfortunate transaction ordering. One possible solution to mitigate this
    /// race condition is to first reduce the spender's allowance to 0 and set the desired value afterwards:
    /// https://github.com/ethereum/EIPs/issues/20#issuecomment-263524729
    /// @param _spender The address which will spend the funds.
    /// @param _value The amount of tokens to be spent.
    function approve(address _spender, uint256 _value) public override _ownerOnly returns (bool) {
        require((_value == 0) || (allowed[msg.sender][_spender] == 0));
        allowed[msg.sender][_spender] = _value;
        return true;
    }

    /// @notice Function to mint tokens
    /// @param _value The amount of tokens to mint.
    /// @param _to The address that will receive the minted tokens.
    /// @return A boolean that indicates if the operation was successful.
    function mint(uint256 _value, address _to) public override returns (bool) {
        totalSupply = totalSupply.add(_value);
        balances[_to] = balances[_to].add(_value);
        emit Mint(_to, _value);
        return true;
    }

    /// @notice Burns a specific amount of tokens from specified address
    /// @param _value The amount of token to be burned.
    /// @param _from The address from which tokens are burned.
    /// @return A boolean that indicates if the operation was successful.
    function burn(uint256 _value, address _from) public override _ownerOnly returns (bool) {
        totalSupply = totalSupply.sub(_value); 
        balances[_from] = balances[_from].sub(_value); 
        emit Burn(_from, _value);
        return true;
    }

    event Mint(address indexed _to, uint256 _value);
    event Burn(address indexed _from, uint256 _value);
}

