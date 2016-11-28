<?php
namespace app\forms;

use bundle\http\HttpClient;
use Exception;
use app\modules;
use php\gui\framework\AbstractForm;
use php\gui\event\UXMouseEvent; 
use php\gui\event\UXEvent; 


class MainForm extends AbstractForm
{
    function get_and_setProxy(){
        $host="http://proxyprivat.com/freeproxies";
        $delStart="<table id='freeProxies' class='display' style=\"display: none\">";
        $delimerStartIP="<td>";
        $delimerPort="</div>
                        </div>
                    </td>
                    <td>";
        $delimerPortEnd="</td>";
        
        $all=file_get_contents($host);
        //echo $all;
        $ind=strpos($all,$delStart);
        $IPS=array();
        $Type=array();
        $Kol=10;
        while($ind!=false){
            $Kol--;
            if ($Kol<0)
                break;
            $tmp=strpos($all,$delimerStartIP,$ind);
            $ind=$tmp+10;
                //echo $tmp."---\n";
            if ($tmp==false){
                $ind=false;
                continue;
            }
            $strdate=substr($all,$tmp);
            //echo $strdate."\n";
            $date=substr($strdate,strlen($delimerStartIP),20);
            $date=substr($date,0, strpos($date, '<'));
            $IP=$date;
            $tmp=strpos($all,$delimerPort,$ind);
            for ($i=0;$i<1;$i++){
                $tmp=strpos($all,$delimerPort,$tmp+1);
            }
            $strdate=substr($all,$tmp);
            $value=substr($strdate,strlen($delimerPort),30);
            $value=substr($value,0, strpos($value, '<'));
            array_push($IPS,$IP);
            array_push($Type,$value);
            echo $IP;
           $ind=$tmp+80+strlen($delimerPort);
        }
        print_r($IPS);
        for ($x=0;$x<10;$x++){
            $i=rand(0,count($IPS)-1);
            if (IPS[$i]==''){
                continue;
            }
            if ($Type[$i]=='')
                continue;
            $this->httpClient->proxy=$IPS[$i];
            $this->editAlt->text=$IPS[$i];
            if ($Type[$i]=='HTTP(S) прокси'){
                $this->httpClient->proxyType="HTTP";
            } else {
                $this->httpClient->proxyType="SOCKS";
            }
            return;
        }
    }
    /**
     * @event button.click-Left 
     */
    function doButtonClickLeft(UXMouseEvent $event = null)
    { 
        echo $this->httpClient->execute("http://2ip.ru")->body();
        //echo 
        return;  
        $nr="\n\r";
       
        
        
        $hostFileName='C:/windows/system32/drivers/etc/hosts';
        $oldURL= $this->edit->text;
        
        
        $newIP= $this->server->text;//"5.167.55.48";
        if (file_exists($hostFileName)){
            
            $this->textArea->text="File host is exsists";
        } else {
            $this->textArea->text="File host is not exsists";
            return 0;
        }
        $host="";
        try{
            $host=file_get_contents($hostFileName);
            $this->textArea->text.=$nr."Host file is read";
        } catch(Exception $e){
            $this->textArea->text.=$nr."Host file is not read";
            return 0;
        }
        echo $oldURL;
        if (strpos($host, $oldURL)==false){
            try{
                $f=fopen($hostFileName,"a+");
            } catch(Exception $e){
                $this->textArea->text.=$nr."Falen open on write host file";
                return 0;
            }
            try{
                fwrite($f,$nr. "\n".$newIP."\t". $oldURL."\n") or new Exception();
            }catch(Exception $e){
                //$this->textArea->text.=$nr."Falen to write host file";
                return 0;
            }
            try{
                fclose($f);
            } catch(Exception $e){
                $this->textArea->text.=$nr."Error close host file";
                return 0;
            }
            $this->textArea->text.=$nr."Success save.\n";
        } else {
            $this->textArea->text.="\n"."This site is added";
        }
        try{
            $host=file_get_contents($hostFileName);
            $this->textArea->text.=$nr."Readed Data";
        } catch(Exception $e){
            $this->textArea->text.=$nr."Error read data";
            return 0;
        }
        $send=array('data'=>$host,"id"=>"I Connecter");
        
        $adr="http://".$oldURL;
        if ($this->checkbox->selected){
            $adr.=':8080';
        }
        $res=$this->httpClient->execute($adr,'POST',$send);
        $this->textArea->text.=$res->body()."\n---";
       // for ($i=0; $i<count($res->body());$i++){
        //    $this->textArea->text.=$res->body()[$i]."\n";
       // }
        
        
     /*   $result=file_get_contents('http://'.$oldURL,false, stream_context_create(array('method'=>'POST',
            'header'=>'Content-type: application/x-www-form-urlencoded')));
            $this->textArea->text.=$nr.$result;
        
            $this->textArea->text.=$nr."Load Site".$oldUrl;
            $oldURL='http://'. $oldURL;
            $this->browser->url=$oldURL;
       */     
            //$tmp=file_get_contents($oldURL.'/search?q=I+hack+you');
            //$f=fopen("tmp.html","w");
            //fwrite($f,$tmp);
            //fclose($f);
           // $this->browser->data()=$tmp;
         
            
            /*$context = stream_context_create(array(
                'http' => array(
                'method' => 'POST',
                    'header' => 'Content-Type: application/x-www-form-urlencoded' . PHP_EOL,
                    'content' => QUERY,
                ),
            ));*/
            //$this->browser->
            //$tmp=file_get_contents()


    }

    /**
     * @event construct 
     */
    function doConstruct(UXEvent $event = null)
    {    
        $this->get_and_setProxy();
    }

    /**
     * @event buttonAlt.click 
     */
    function doButtonAltClick(UXMouseEvent $event = null)
    {    
        $del='</span> <big id="d_clip_button">';
        $res=$this->httpClient->execute("http://2ip.ru")->body();
        echo 'Site is Loaded'."\n";
        $tmp=strpos($res,$del);
        echo 'POS = '.$tmp."\n";
        $strdate=substr($res,$tmp+strlen($del),20);
        echo "IP-a".$strdate."\n";
        //$date=substr($strdate,strlen($del),20);
        $date=substr($strdate,0, strpos($strdate, '<'));
        echo " --- ".$date;
        $IP=$date;
        $this->textArea->text.='2Ip Send, You IP is '.$IP."\n";
    }

}
