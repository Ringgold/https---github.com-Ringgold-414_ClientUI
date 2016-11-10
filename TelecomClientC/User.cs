using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class User
{
    private String userName;
    private String publicIp;
    private String privateIp;
    private int privatePort;
    private int publicPort;

    public User(String publicIp, int publicPort, String privateIp, int privatePort, String userName)
    {
        this.userName = userName;
        this.publicIp = publicIp;
        this.publicPort = publicPort;
        this.privateIp = privateIp;
        this.privatePort = privatePort;
    }

    public String getUserName()
    {
        return userName;
    }

    public void setUserName(String userName)
    {
        this.userName = userName;
    }

    public String getPublicIp()
    {
        return publicIp;
    }

    public void setPublicIp(String publicIp)
    {
        this.publicIp = publicIp;
    }

    public String getPrivateIp()
    {
        return privateIp;
    }

    public void setPrivateIp(String privateIp)
    {
        this.privateIp = privateIp;
    }

    public int getPrivatePort()
    {
        return privatePort;
    }

    public void setPrivatePort(int privatePort)
    {
        this.privatePort = privatePort;
    }

    public int getPublicPort()
    {
        return publicPort;
    }

    public void setPublicPort(int publicPort)
    {
        this.publicPort = publicPort;
    }
}
